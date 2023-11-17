using CarHandbook.API.Models;
using CarHandbook.API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarHandbook.API.Controllers
{
    [Route("api/models")]
    [ApiController]
    public class ModelsController : ControllerBase
    {
        private readonly IModelsRepository _modelsRepository;

        public ModelsController(IModelsRepository modelsRepository) => _modelsRepository = modelsRepository;

        [HttpGet]
        public async Task<IActionResult> GetModels()
        {
            var models = await _modelsRepository.GetModels();
            if (models == null) 
                return NotFound();

            return Ok(models);
        }

        [HttpPost]
        public async Task<IActionResult> CreateModel([FromBody]Model model)
        {
            var createdModelId = await _modelsRepository.CreateModel(model);
            model.Id = createdModelId;

            return CreatedAtRoute("BrandById", new { id = createdModelId }, model);
        }

        [HttpGet("{id}", Name = "ModelById")]
        public async Task<IActionResult> GetModelById(int id)
        {
            var model = await _modelsRepository.GetModelById(id);
            if (model == null)
                return NotFound();

            return Ok(model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModel(int id)
        {
            var model = _modelsRepository.GetModelById(id).Result;
            if (model is null)
                return NotFound();

            await _modelsRepository.DeleteModel(id);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateModel(int id, [FromBody] Model updModel)
        {
            var model = _modelsRepository.GetModelById(id).Result;
            if (model is null)
                return NotFound();

            await _modelsRepository.UpdateModel(id, updModel);

            return NoContent();
        }
    }
}
