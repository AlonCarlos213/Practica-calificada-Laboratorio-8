using Lab_8___Carlos_Mamani.Models;

namespace Lab_8___Carlos_Mamani.Repositories.Interfaces;

public interface IProductRepository
{
    Task<List<Product>> GetByMinPriceAsync(decimal minPrice, CancellationToken ct = default);
    Task<Product?> GetMostExpensiveAsync(CancellationToken ct = default);
    Task<decimal> GetAveragePriceAsync(CancellationToken ct = default);
    Task<List<Product>> GetWithoutDescriptionAsync(CancellationToken ct = default);

    // Clientes que compraron un producto
    Task<List<Orderdetail>> GetClientsByProductAsync(int productId, CancellationToken ct = default);
}