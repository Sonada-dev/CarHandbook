using CarHandbook.API.Models;

namespace CarHandbook.API.Repository
{
    public interface IBrandsRepository
    {
        public Task<IEnumerable<Brand>> GetBrands();
        public Task<Brand?> GetBrandById(int id);
        public Task<int> CreateBrand(Brand brand);
        public Task UpdateBrand(int id,Brand brand);
        public Task DeleteBrand(int id);
        public Task<IEnumerable<Brand>> GetBrandsWithModels();
    }
}
