using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CarHandbook.API.Models
{
    public class Brand
    {
        [Display(Name = "ID")]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Display(Name = "Название")]
        [JsonPropertyName("name")]
        [Required(ErrorMessage = "Обязательное поле")]
        public string? Name { get; set; }

        [Display(Name = "Активен")]
        [JsonPropertyName("active")]
        [Required(ErrorMessage = "Обязательное поле")]
        public bool Active { get; set; }

        [JsonPropertyName("models")]
        public List<Model?> Models { get; set; } = new List<Model?>();
    }
}
