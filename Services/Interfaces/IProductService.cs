using Lab_8___Carlos_Mamani.DTOs;

namespace Lab_8___Carlos_Mamani.Services.Interfaces;

public interface IProductService
{
    Task<List<ProductDto>> GetByMinPriceAsync(decimal price);
    Task<ProductDto?> GetMostExpensiveAsync();
    Task<decimal> GetAveragePriceAsync();
    Task<List<ProductDto>> GetWithoutDescriptionAsync();
    Task<object> GetClientsByProductAsync(int productId); // igual, puedes crear un DTO propio
}