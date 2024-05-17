using DocumentFormat.OpenXml.Presentation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LLMEducation.Data.Entity
{
    public class TestQ
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        public string Id { get; set; }
        [JsonPropertyName("question")]
        public string QuestionText { get; set; }
        [JsonPropertyName("A")]
        public string OptionA { get; set; }
        [JsonPropertyName("B")]
        public string OptionB { get; set; }
        [JsonPropertyName("C")]
        public string OptionC { get; set; }
        [JsonPropertyName("D")]
        public string OptionD { get; set; }
        [JsonPropertyName("correct_answer_letter")]
        public string CorrectAnswerLetter { get; set; }
        [JsonPropertyName("correct_answer_explanation")]
        public string CorrectAnswerExplanation { get; set; }
        public ModelType modelType { get; set; }

        public string contentId { get; set; }
        public Content content { get; set; } 
    }
}
