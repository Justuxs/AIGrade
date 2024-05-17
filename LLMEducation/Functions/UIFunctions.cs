using System;

namespace LLMEducation.Functions
{
    public class UIFunctions
    {
        private static Random random = new Random();

        public static string GetRandomColor()
        {
            int numberFrom = 80;
            int numberTo = 240;
            var color = $"rgb({random.Next(numberFrom, numberTo)}, {random.Next(numberFrom, numberTo)}, {random.Next(numberFrom, numberTo)}";
            return color;
        }
        public static string GetCorrectAnswerColor()
        {
            return "#75DF8B";
        }

        public static string GetWrongAnswerColor()
        {
            return "#";
        }
        public static string GetSelcetedAnswerColor()
        {
            return "#ff6054";
        }
    }
}
