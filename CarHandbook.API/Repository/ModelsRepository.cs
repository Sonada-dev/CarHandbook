using CarHandbook.API.Context;
using CarHandbook.API.Models;
using Dapper;

namespace CarHandbook.API.Repository
{
    public class ModelsRepository : IModelsRepository
    {
        private readonly DapperContext _dapperContext;

        public ModelsRepository(DapperContext dapperContext) => _dapperContext = dapperContext;

        public async Task<int> CreateModel(Model model)
        {
            string query = $"INSERT INTO Models (Name, Active, BrandId) output inserted.Id " +
               $"VALUES (@{nameof(Model.Name)}, @{nameof(Model.Active)}, @{nameof(Model.BrandId)})";

            using var connection = _dapperContext.CreateConnection();

            var id = await connection.QuerySingleAsync<int>(query, model);

            return id;
        }

        public async Task DeleteModel(int id)
        {
            var query = $"DELETE FROM Models WHERE Id = @id";

            using var connection = _dapperContext.CreateConnection();

            await connection.ExecuteAsync(query, new { id });
        }

        public async Task<Model?> GetModelById(int id)
        {
            string query = $"SELECT Id as {nameof(Model.Id)}, " +
                 $"Name as {nameof(Model.Name)}, " +
                 $"Active as {nameof(Model.Active)}, " +
                 $"BrandId as {nameof(Model.BrandId)} FROM Models " +
                 $"WHERE Id = @{nameof(Model.Id)}";

            using var connection = _dapperContext.CreateConnection();

            var model = await connection.QuerySingleOrDefaultAsync<Model>(query, new { id });

            return model;
        }

        public async Task<IEnumerable<Model>> GetModels()
        {
            string query = $"SELECT Id as {nameof(Model.Id)}, " +
                $"Name as {nameof(Model.Name)}, " +
                $"Active as {nameof(Model.Active)}," +
                $"BrandId as {nameof(Model.BrandId)} FROM Models";

            using var connection = _dapperContext.CreateConnection();

            var models = await connection.QueryAsync<Model>(query);

            return models.ToList();
        }

        public async Task UpdateModel(int id, Model model)
        {
            string query = $"UPDATE Models SET Name = @{nameof(Model.Name)}, Active = @{nameof(Model.Active)}, BrandId = @{nameof(Model.BrandId)}" +
                $" WHERE Id=@id";

            using var connection = _dapperContext.CreateConnection();

            await connection.ExecuteAsync(query, new { id, model.Name, model.Active, model.BrandId });
        }
    }
}
