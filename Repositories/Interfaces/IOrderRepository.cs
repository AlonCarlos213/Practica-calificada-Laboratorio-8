using Lab_8___Carlos_Mamani.Models;

namespace Lab_8___Carlos_Mamani.Repositories.Interfaces;

public interface IOrderRepository
{
    // Para /api/pedidos/agrupados?date=...
    Task<List<(int ClientId, int Count)>> GetAfterDateAsync(DateTime fecha, CancellationToken ct = default);

    // Para /api/pedidos/detalles
    Task<List<Orderdetail>> GetAllDetailsAsync(CancellationToken ct = default);

    // Cliente con más pedidos: ya devolvemos el NOMBRE del cliente y su total
    Task<(string Cliente, int TotalPedidos)?> GetTopClientAsync(CancellationToken ct = default);
}