using System.Text.Json.Serialization;

namespace CarHandbook.API.Models
{
    public class Model
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("active")]
        public bool Active { get; set; }
        [JsonPropertyName("brandId")]
        public int BrandId { get; set; }

    }
}
