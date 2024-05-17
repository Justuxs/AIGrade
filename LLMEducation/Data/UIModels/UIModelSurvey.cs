namespace LLMEducation.Data.UIModels
{
    public class UIModelSurvey
    {
        public int Rating1 = 0;
        public int Rating2 = 0;
        public int Rating3 = 0;
        public int Rating4 = 0;
        public bool isNotFilled()
        {
            return (Rating1 == 0
                || Rating2 == 0
                || Rating3 == 0
                || Rating4 == 0);
        }
    }
}
