using CarHandbook.API.Context;
using CarHandbook.API.Models;
using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Data;

namespace CarHandbook.API.Repository
{
    public class BrandsRepository : IBrandsRepository
    {
        private readonly DapperContext _dapperContext;

        public BrandsRepository(DapperContext dapperContext) => _dapperContext = dapperContext;

        public async Task<IEnumerable<Brand>> GetBrands()
        {
            string query = $"SELECT Id as {nameof(Brand.Id)}, " +
                $"Name as {nameof(Brand.Name)}, " +
                $"Active as {nameof(Brand.Active)} FROM Brands";

            using var connection = _dapperContext.CreateConnection();
            
            var brands = await connection.QueryAsync<Brand>(query);

            return brands.ToList();
        }

        public async Task<Brand?> GetBrandById(int id)
        {
            string query = $"SELECT Id as {nameof(Brand.Id)}, " +
                 $"Name as {nameof(Brand.Name)}, " +
                 $"Active as {nameof(Brand.Active)} FROM Brands " +
                 $"WHERE Id = @{nameof(Brand.Id)}";

            using var connection = _dapperContext.CreateConnection();

            var brand = await connection.QuerySingleOrDefaultAsync<Brand>(query, new {id});

            return brand;
        }

        public async Task<int> CreateBrand(Brand brand)
        {
            string query = $"INSERT INTO Brands (Name, Active) output inserted.Id " +
                $"VALUES (@{nameof(Brand.Name)}, @{nameof(Brand.Active)})";

            using var connection = _dapperContext.CreateConnection();

            var id = await connection.QuerySingleAsync<int>(query, brand);

            return id;
        }

        public async Task UpdateBrand(int id, Brand brand)
        {
            string query = $"UPDATE Brands SET Name = @{nameof(Brand.Name)}, Active = @{nameof(Brand.Active)} WHERE Id=@id";

            using var connection = _dapperContext.CreateConnection();

            await connection.ExecuteAsync(query, new {id, brand.Name, brand.Active});
        }

        public async Task DeleteBrand(int id)
        {
            var query = $"DELETE FROM Brands WHERE Id = @id";

            using var connection = _dapperContext.CreateConnection();

            await connection.ExecuteAsync(query, new { id });
        }

        public async Task<IEnumerable<Brand>> GetBrandsWithModels()
        {
            var procedure = "GetBrandsWithModels";

            using var connection = _dapperContext.CreateConnection();

            var brands = await connection.QueryAsync<Brand, Model, Brand>(procedure,(brand, model) =>
            {
                brand.Models.Add(model);
                return brand;
            },
            commandType: CommandType.StoredProcedure, splitOn: "Id");

            return brands;
        }
    }
}
