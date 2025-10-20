using Lab_8___Carlos_Mamani.DTOs;

namespace Lab_8___Carlos_Mamani.Services.Interfaces;

public interface IOrderService
{
    Task<List<OrderWithCountDto>> GetAfterDateAsync(DateTime date);
    Task<List<OrderDetailDto>> GetAllDetailsAsync();
    Task<(string Cliente, int TotalPedidos)?> GetTopClientAsync();
}