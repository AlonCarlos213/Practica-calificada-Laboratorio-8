using Lab_8___Carlos_Mamani.Models;
using Lab_8___Carlos_Mamani.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lab_8___Carlos_Mamani.Repositories.Implementations;

public class ProductRepository : IProductRepository
{
    private readonly LINQExampleContext _db;
    public ProductRepository(LINQExampleContext db) => _db = db;

    public Task<List<Product>> GetByMinPriceAsync(decimal minPrice, CancellationToken ct = default) =>
        _db.Products.Where(p => p.Price > minPrice).ToListAsync(ct);

    public Task<Product?> GetMostExpensiveAsync(CancellationToken ct = default) =>
        _db.Products.OrderByDescending(p => p.Price).FirstOrDefaultAsync(ct);

    public async Task<decimal> GetAveragePriceAsync(CancellationToken ct = default) =>
        await _db.Products.Select(p => p.Price).AverageAsync(ct);

    public Task<List<Product>> GetWithoutDescriptionAsync(CancellationToken ct = default) =>
        _db.Products.Where(p => p.Description == null || p.Description == "").ToListAsync(ct);

    public Task<List<Orderdetail>> GetClientsByProductAsync(int productId, CancellationToken ct = default) =>
        _db.Orderdetails
            .Include(od => od.Product)
            .Include(od => od.Order).ThenInclude(o => o.Client)
            .Where(od => od.Productid == productId)
            .ToListAsync(ct);
}