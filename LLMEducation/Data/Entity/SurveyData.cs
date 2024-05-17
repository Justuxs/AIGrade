using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LLMEducation.Data.Entity
{
    public class SurveyData
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        public string Id { get; set; }
        public string? UserID { get; set; }
        public DateTime TimeStamp_ { get; set; } = DateTime.UtcNow;
        public string? TestQMistralId { get; set; }
        public string? TestLlamaId { get; set; }
        public string? TestQGemmaId { get; set; }

        public int? MistralSurvey1 { get; set; }
        public int? MistralSurvey2 { get; set; }
        public int? MistralSurvey3 { get; set; }
        public int? MistralSurvey4 { get; set; }

        public int? LLamaSurvey1 { get; set; }
        public int? LLamaSurvey2 { get; set; }
        public int? LLamaSurvey3 { get; set; }
        public int? LLamaSurvey4 { get; set; }

        public int? GemmaSurvey1 { get; set; }
        public int? GemmaSurvey2 { get; set; }
        public int? GemmaSurvey3 { get; set; }
        public int? GemmaSurvey4 { get; set; }

        public int? LastSurvey1 { get; set; }
        public int? LastSurvey2 { get; set; }
        public int? LastSurvey3 { get; set; }
        public int? LastSurvey4 { get; set; }
    }
}
