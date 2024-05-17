using DocumentFormat.OpenXml.Presentation;
using LLMEducation.Data.Entity;
using LLMEducation.Data.Migrations;
using LLMEducation.Data.UIModels;
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
    public class SurveyService
    {

        public readonly ContentRepo contentRepo;
        public readonly TestQRepo testQRepo;

        public SurveyService(ContentRepo _contentRepo, TestQRepo _testQRepo)
        {
            contentRepo = _contentRepo;
            testQRepo = _testQRepo;
        }

        public async Task<UIRating> FormateUIModelRating(SubjectTypes subjectTypes, bool retry = true)
        {
            Content randomContent = await contentRepo.GetAllContentBySubjectRandom(subjectTypes);
            TestQ mistral = await testQRepo.GetRandomByContentAndModel(randomContent.Id, ModelType.Mistral);
            TestQ llama = await testQRepo.GetRandomByContentAndModel(randomContent.Id, ModelType.LLama);
            TestQ gemma = await testQRepo.GetRandomByContentAndModel(randomContent.Id, ModelType.Gemma);

            if (gemma == null || llama == null || mistral ==null)
            {
                if (retry)
                {
                    return await FormateUIModelRating(subjectTypes, false);
                }
                else
                {
                    return null;
                }
            }

            mistral.content= randomContent;
            llama.content= randomContent;
            gemma.content= randomContent;

            UIRating rating = new UIRating() { 
                GemmaQuiz = new UIQuiz(gemma),
                LlamaQuiz = new UIQuiz(llama),
                MistralQuiz = new UIQuiz(mistral)
            };

            return rating;
        }
    }
}
