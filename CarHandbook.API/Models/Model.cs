using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CarHandbook.API.Models
{
    public class Model
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        [Required(ErrorMessage = "Обязательное поле")]
        public string? Name { get; set; }
        [JsonPropertyName("active")]
        [Required(ErrorMessage = "Обязательное поле")]
        public bool Active { get; set; }
        [JsonPropertyName("brandId")]
        [Required(ErrorMessage = "Обязательное поле")]
        public int BrandId { get; set; }

    }
}
