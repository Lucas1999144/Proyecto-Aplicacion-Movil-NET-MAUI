using SQLite;
using Parcial_Moviles.Models;

namespace Parcial_Moviles.Services
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Product>().Wait();
        }

        public Task<List<Product>> GetProductsAsync()
        {
            return _database.Table<Product>().ToListAsync();
        }

        public Task<int> SaveProductAsync(Product product)
        {
            return _database.InsertOrReplaceAsync(product);
        }

        public async Task SaveAllProductsAsync(List<Product> products)
        {
            foreach (var product in products)
                await _database.InsertOrReplaceAsync(product);
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _database.Table<Product>()
                                  .Where(p => p.Id == id)
                                  .FirstOrDefaultAsync();
        }
    }
}