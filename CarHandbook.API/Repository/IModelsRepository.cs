using CarHandbook.API.Models;

namespace CarHandbook.API.Repository
{
    public interface IModelsRepository
    {
        public Task<IEnumerable<Model>> GetModels();
        public Task<Model?> GetModelById(int id);
        public Task<int> CreateModel(Model model);
        public Task UpdateModel(int id, Model model);
        public Task DeleteModel(int id);
    }
}
