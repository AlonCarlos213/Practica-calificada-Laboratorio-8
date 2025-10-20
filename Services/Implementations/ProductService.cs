using AutoMapper;
using Lab_8___Carlos_Mamani.DTOs;
using Lab_8___Carlos_Mamani.Services.Interfaces;
using Lab_8___Carlos_Mamani.Repositories.Interfaces;

namespace Lab_8___Carlos_Mamani.Services.Implementations;

public class ProductService : IProductService
{
    private readonly IProductRepository _products;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository products, IMapper mapper)
    {
        _products = products;
        _mapper = mapper;
    }

    public async Task<List<ProductDto>> GetByMinPriceAsync(decimal price)
    {
        var entities = await _products.GetByMinPriceAsync(price);
        return _mapper.Map<List<ProductDto>>(entities);
    }

    public async Task<ProductDto?> GetMostExpensiveAsync()
    {
        var entity = await _products.GetMostExpensiveAsync();
        return _mapper.Map<ProductDto?>(entity);
    }

    public Task<decimal> GetAveragePriceAsync()
        => _products.GetAveragePriceAsync();

    public async Task<List<ProductDto>> GetWithoutDescriptionAsync()
    {
        var entities = await _products.GetWithoutDescriptionAsync();
        return _mapper.Map<List<ProductDto>>(entities);
    }

    public async Task<object> GetClientsByProductAsync(int productId)
    {
        var rows = await _products.GetClientsByProductAsync(productId);
        return rows;
    }
}