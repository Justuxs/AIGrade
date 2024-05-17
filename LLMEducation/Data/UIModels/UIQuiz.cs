using DocumentFormat.OpenXml.Presentation;
using LLMEducation.Data.Entity;

namespace LLMEducation.Data.UIModels
{
    public class UIQuiz
    {
        public bool isAnswered = false;

        public bool isCorrect = false;
        public TestQ testQ { get; set; }

        public UIQuiz(TestQ testQ)
        {
            this.testQ = testQ;
        }
        public UIQuiz() { }
    }
}
