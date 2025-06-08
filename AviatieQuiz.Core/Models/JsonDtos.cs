// -----------------------------------------------------------------------
// Autor: Agape Ioan
// Data: 2025-05-27
// Descriere: 
// -----------------------------------------------------------------------



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace AviatieQuiz.Core.Models.JsonDtos
{
    public class JsonDiscipline
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Questions")]
        public List<JsonQuestion> Questions { get; set; }
    }

    public class JsonQuestion
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("Text")]
        public string Text { get; set; }

        [JsonPropertyName("Options")]
        public List<JsonAnswerOption> Options { get; set; }

        [JsonPropertyName("CorrectOptionId")]
        public int CorrectOptionId { get; set; }
    }

    public class JsonAnswerOption
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("Text")]
        public string Text { get; set; }
    }
}