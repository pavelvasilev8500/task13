using System;
using System.Text.Json.Serialization;

namespace Library
{
    public class Book
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("author")]
        public string Author { get; set; }
        [JsonPropertyName("year")]
        public int Year { get; set; }

    }
}
