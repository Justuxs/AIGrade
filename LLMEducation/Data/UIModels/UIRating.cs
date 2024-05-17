namespace LLMEducation.Data.UIModels
{
    public class UIRating
    {
        public string User;
        public UIQuiz MistralQuiz { get; set; }
        public UIQuiz LlamaQuiz { get; set; }
        public UIQuiz GemmaQuiz { get; set; }

        public UIModelSurvey MistralSurvey = new();
        public UIModelSurvey LammaSurvey = new();
        public UIModelSurvey GemmaSurvey = new();

        public UIModelSurvey LastSurvey = new();

    }
}
