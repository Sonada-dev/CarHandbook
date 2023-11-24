using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CarHandbook.API.Models
{
    public class Brand
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        [Required(ErrorMessage = "Обязательное поле")]
        public string? Name { get; set; }
        [JsonPropertyName("active")]
        [Required(ErrorMessage = "Обязательное поле")]
        public bool Active { get; set; }
        [JsonPropertyName("models")]
        public List<Model?> Models { get; set; } = new List<Model?>();
    }
}
