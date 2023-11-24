using CarHandbook.API.Models;
using CarHandbook.API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarHandbook.API.Controllers
{
    [Route("api/brands")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandsRepository _brandRepository;

        public BrandsController(IBrandsRepository brandRepository) => _brandRepository = brandRepository;

        [HttpGet]
        public async Task<IActionResult> GetBrands()
        {
            var brands = await _brandRepository.GetBrands();
            if (brands is null)
                return NotFound();

            return Ok(brands);
        }

        [HttpGet("{id}", Name = "BrandById")]
        public async Task<IActionResult> GetBrandById(int id)
        {
            var brand = await _brandRepository.GetBrandById(id);
            if (brand == null)
                return NotFound();

            return Ok(brand);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBrand([FromBody] Brand brand)
        {
            var createdBrandId = await _brandRepository.CreateBrand(brand);
            brand.Id = createdBrandId;

            return CreatedAtRoute("BrandById", new { id = createdBrandId }, brand);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBrand(int id,[FromBody] Brand updBrand)
        {
            var brand = await _brandRepository.GetBrandById(id);
            if (brand is null)
                return NotFound();

            await _brandRepository.UpdateBrand(id,updBrand);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            var brand = await _brandRepository.GetBrandById(id);
            if (brand is null)
                return NotFound();

            await _brandRepository.DeleteBrand(id);

            return NoContent();
        }

        [HttpGet("brands-with-models")]
        public async Task<IActionResult> GetBrandsWithModels()
        {
            var brands = await _brandRepository.GetBrandsWithModels();
            if(brands is null)
                return NotFound();

            return Ok(brands);
        }
    }
}
