using Parcial_Moviles.Models;

namespace Parcial_Moviles.Services;

public interface IApiService
{   
    Task<List<Product>> GetProductsAsync(CancellationToken ct = default);
    Task<Product?> GetProductByIdAsync(int id, CancellationToken ct = default);
}