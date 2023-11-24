using CarHandbook.API.Models;
using System.ComponentModel.DataAnnotations;

namespace CarHandbook.Web.Models
{
    public class BrandsModelViewModel
    {
        public IEnumerable<Brand> brands { get; set; } = new List<Brand>();
        public Model? Model { get; set; }
    }
}
