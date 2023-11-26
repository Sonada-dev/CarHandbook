using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CarHandbook.API.Models
{
    public class Model
    {
        [Display(Name = "ID")]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Display(Name = "Название")]
        [JsonPropertyName("name")]
        [Required(ErrorMessage = "Обязательное поле")]
        public string? Name { get; set; }

        [Display(Name = "Активность")]
        [JsonPropertyName("active")]
        [Required(ErrorMessage = "Обязательное поле")]
        public bool Active { get; set; }


        [Display(Name = "ID Марки")]
        [JsonPropertyName("brandId")]
        [Required(ErrorMessage = "Обязательное поле")]
        public int BrandId { get; set; }

    }
}
