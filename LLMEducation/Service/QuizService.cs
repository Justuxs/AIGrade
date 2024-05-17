using LLMEducation.Data.Entity;
using LLMEducation.Data.UIModels;
using LLMEducation.Repos;

namespace LLMEducation.Service
{
    public class QuizService
    {

        public readonly ContentRepo contentRepo;
        public readonly TestQRepo testQRepo;

        public List<Content> contents = new List<Content>();

        public QuizService(ContentRepo _contentRepo, TestQRepo _testQRepo) { 
            contentRepo = _contentRepo;
            testQRepo = _testQRepo;
        }

        public async Task<List<string>> GetContentTheme(SubjectTypes subjectTypes)
        {
            contents = await contentRepo.GetAllContentBySubject(subjectTypes);
            List<string> result = new List<string>();
            foreach (Content content in contents)
            {
                result.Add(content.Theme);
            }
            result = result.Distinct().ToList();
            return result;
        }

        public List<string> GetContentTopic(string theme)
        {
            var contents_ = contents.Where(x => x.Theme == theme).ToList();
            List<string> result = new List<string>();
            foreach (Content content in contents_)
            {
                result.Add(content.Topic);
            }
            result = result.Distinct().ToList();
            return result;
        }


        public async Task<List<TestQ>> GetTestQs(string contentId)
        {
            
            return await testQRepo.GetTestQs(contentId);
        }


        public async Task<List<TestQ>> GetFiltatedTestQs(string theme, string topic, int qty = 10)
        {
            var contents_ = contents.Where(x => x.Theme == theme && topic == x.Topic).ToList();

            if (contents_.Count > 10)
            {
                var random = new Random();
                contents_ = contents_.OrderBy(x => random.Next())
                                    .Take(10)
                                    .ToList();
            }
            List<string> ids = new List<string>();
            foreach (Content content in contents_)
            {
                ids.Add(content.Id);
            }
            return await testQRepo.GetTestQs(ids);
        }

        public async Task<List<UIQuiz>> GetFiltatedQuizes (string theme, string topic)
        {
            List<UIQuiz> quizzes = new List<UIQuiz>();

            var testQs = await GetFiltatedTestQs(theme, topic);
            
            foreach (TestQ testq in testQs)
            {
                quizzes.Add(new UIQuiz(testq));
            }
            return quizzes;
        }


        public async Task<TestQ?> SaveTestQs(TestQ testQ)
        {
            try
            {
                return await testQRepo.SaveTestQ(testQ);

            }catch (Exception ex) { return null; }
        }

    }

}
