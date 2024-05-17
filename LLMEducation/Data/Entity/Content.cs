using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LLMEducation.Data.Entity
{
    public class Content
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        public string Id { get; set; }
        public string Subject { get; set; }
        public string Theme { get; set; }
        public string Topic { get; set; }
        public string Subtopic { get; set; } = string.Empty;
    }
}
