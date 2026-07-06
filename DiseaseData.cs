using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SimptomChek
{
    public class DiseaseCategory
    {
        [JsonPropertyName("Category")]
        public string Category { get; set; }

        [JsonPropertyName("Diseases")]
        public List<Disease> Diseases { get; set; }
    }

    public class Disease
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Symptoms")]
        public string[] Symptoms { get; set; }

        [JsonPropertyName("Questions")]
        public Question[] Questions { get; set; }

        [JsonPropertyName("Actions")]
        public string[] Actions { get; set; }

        [JsonPropertyName("Severity")]
        public string Severity { get; set; }

        [JsonPropertyName("Gender")]
        public string Gender { get; set; }
    }

    public class Question
    {
        [JsonPropertyName("Text")]
        public string Text { get; set; }

        [JsonPropertyName("Worsens")]
        public bool Worsens { get; set; }
    }
}