namespace CarHandbook.API.Models
{
    public class Brand
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool Active { get; set; }
        public List<Model> Models { get; set; } = new List<Model>();
    }
}
