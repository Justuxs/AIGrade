using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Presentation;
using LLMEducation.Data.Entity;
using LLMEducation.Data.Migrations;
using LLMEducation.Repos;
using Newtonsoft.Json;
using OfficeOpenXml.FormulaParsing.LexicalAnalysis;
using System;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.Json;

namespace LLMEducation.Service
{
    public class LLMAPI
    {

        public readonly ContentRepo contentRepo;
        public readonly TestQRepo testQRepo;
        public LLMAPI(ContentRepo _contentRepo, TestQRepo _testQRepo)
        {
            contentRepo = _contentRepo;
            testQRepo = _testQRepo;
        }

        public async Task FillTestQ(ModelType modelType = ModelType.LLama)
        {

            await FillTestQs(3, SubjectTypes.Biology, modelType);
            await FillTestQs(3, SubjectTypes.Physics, modelType);
            await FillTestQs(5, SubjectTypes.Chemistry, modelType);

        }

        public async Task FillTestQs(int questionsForTopic = 3, SubjectTypes subject = SubjectTypes.Biology, ModelType modelType = ModelType.Mistral)
        {
            List<Content> contents = await contentRepo.GetAllContentBySubject(subject);
            foreach (Content content in contents)
            {
                int generatedTests = testQRepo.CountTestsForContent(content.Id, modelType);
                int questionsForTopicNeedToGenerate = questionsForTopic - generatedTests;
                int i = 0;
                while(i< questionsForTopicNeedToGenerate)
                {
                    if(await GenerateTestQWithSave(modelType, content))
                    {
                        i++;
                    }
                }
            }
        }


        public async Task<bool> GenerateTestQWithSave(ModelType modelType, Content content = null)
        {
            try
            {
                if (content == null)
                {
                    var contents = (await contentRepo.GetAllContent()).FirstOrDefault();
                }

                string topic = $"{content.Subject} in {content.Theme} about {content.Topic} {content.Subtopic}";
                TestQ testQ = await CallDeepinfra(topic, modelType);
                if (testQ != null)
                {
                    testQ.contentId = content.Id;
                    testQ.modelType = modelType;
                    await testQRepo.SaveTestQ(testQ);
                    return true;
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public async Task<TestQ?> CallDeepinfra(string topic,ModelType modelType)
        {
            string template = @"
                <s>[INST]
                ###Instruction###
                You MUST formulate 1 single multiple-choice test
                question on a topic: {context}.
                This test MUST be oriented for 7-8 grade student.
                International system of units MUST be used for units
                of measurement.
                You MUST follow JSON schema format.
                You will be penalized for more than one correct answer
                in test.
                Ensure that your answer is unbiased and avoids relying
                on stereotypes. [/INST]";

            string formattedTemplate = template.Replace("{context}", topic);


            string modelPath = "";
            switch (modelType)
            {
                case ModelType.Mistral:
                    modelPath = "mistralai/Mistral-7B-Instruct-v0.1";
                    break;
                case ModelType.Gemma:
                    modelPath = "google/gemma-7b-it";
                    break;
                case ModelType.LLama:
                    modelPath = "meta-llama/Llama-2-7b-chat-hf";
                    break;
            }
            var url = "https://api.deepinfra.com/v1/openai/chat/completions";
            var body = new
            {
                model = modelPath,

                messages = new[]
                {
                new { role = "system", content = "Response foramt is in json: " },
                new
                {
                    role = "user",
                    content = formattedTemplate
                }
            },
                stream = false
            };

            using (var httpClient = new HttpClient())
            {
                var jsonBody = Newtonsoft.Json.JsonConvert.SerializeObject(body);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                string token = "your_token";

                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await httpClient.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    dynamic responseData = Newtonsoft.Json.JsonConvert.DeserializeObject(responseContent);
                    string messageContent = responseData.choices[0].message.content;

                    TestQ? testQ = PareseResponse(messageContent);
                    return testQ;

                }
                else
                {
                    Console.WriteLine("Request failed with status code: " + response.StatusCode);
                    return null;
                }
            }
        }

        public TestQ? PareseResponse(string msg)
        {
            TestQ? res = null;
            JsonElement root;
            try
            {
                int startIndex = msg.IndexOf('{');
                int endIndex = msg.LastIndexOf('}');

                if (startIndex != -1 && endIndex != -1)
                {
                    string jsonObject = msg.Substring(startIndex, endIndex - startIndex + 1);

                    root = JsonDocument.Parse(jsonObject).RootElement;

                    string a = root.GetProperty("A").GetString();
                    string b = root.GetProperty("B").GetString();
                    string c = root.GetProperty("C").GetString();
                    string d = root.GetProperty("D").GetString();
                    string question = root.GetProperty("question").GetString();
                    string correctAnswerLetter = root.GetProperty("correct_answer_letter").GetString();
                    string correcrAnswerExplaination = root.GetProperty("correct_answer_explanation").GetString();


                    bool allPropertiesValid = !string.IsNullOrEmpty(a) &&
                                             !string.IsNullOrEmpty(b) &&
                                             !string.IsNullOrEmpty(c) &&
                                             !string.IsNullOrEmpty(d) &&
                                             !string.IsNullOrEmpty(question) &&
                                             !string.IsNullOrEmpty(correctAnswerLetter) &&
                                             !string.IsNullOrEmpty(correcrAnswerExplaination);

                    if (allPropertiesValid)
                    {
                        res = new()
                        {
                            CorrectAnswerExplanation = correcrAnswerExplaination,
                            CorrectAnswerLetter = correctAnswerLetter,
                            OptionA = a,
                            OptionB = b,
                            OptionC = c,
                            OptionD = d,
                            QuestionText = question
                        };
                    }

                    return res;
                }
            }
            catch (Exception e)
            {

            }

            return res;
        }

        public async Task<TestQ?> GenerateTestQ(ModelType modelType, Content content)
        {
            try
            {
                if (content == null)
                {
                    return null;
                }

                string topic = $"{content.Subject} in {content.Theme} about {content.Topic} {content.Subtopic}";
                TestQ testQ = await CallDeepinfra(topic, modelType);
                return testQ;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


    }

}
