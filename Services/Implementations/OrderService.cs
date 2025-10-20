using AutoMapper;
using Lab_8___Carlos_Mamani.DTOs;
using Lab_8___Carlos_Mamani.Services.Interfaces;
using Lab_8___Carlos_Mamani.Repositories.Interfaces;

namespace Lab_8___Carlos_Mamani.Services.Implementations;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orders;
    private readonly IMapper _mapper;

    public OrderService(IOrderRepository orders, IMapper mapper)
    {
        _orders = orders;
        _mapper = mapper;
    }

    public async Task<List<OrderWithCountDto>> GetAfterDateAsync(DateTime date)
    {
        var tuples = await _orders.GetAfterDateAsync(date); // List<(ClientId, Count)>
        return _mapper.Map<List<OrderWithCountDto>>(tuples);
    }

    public async Task<List<OrderDetailDto>> GetAllDetailsAsync()
    {
        var details = await _orders.GetAllDetailsAsync(); // ya incluye Product y Client
        return _mapper.Map<List<OrderDetailDto>>(details);
    }

    public Task<(string Cliente, int TotalPedidos)?> GetTopClientAsync()
        => _orders.GetTopClientAsync();
}