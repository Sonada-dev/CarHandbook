using System.Text.Json.Serialization;

namespace CarHandbook.API.Models
{
    public class Brand
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("active")]
        public bool Active { get; set; }
        [JsonPropertyName("models")]
        public List<Model?> Models { get; set; } = new List<Model?>();
    }
}
