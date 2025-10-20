using Lab_8___Carlos_Mamani.Models;
using Lab_8___Carlos_Mamani.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lab_8___Carlos_Mamani.Repositories.Implementations;

public class OrderRepository : IOrderRepository
{
    private readonly LINQExampleContext _db;
    public OrderRepository(LINQExampleContext db) => _db = db;

    // Devuelve (ClientId, Count) para que el servicio lo mapee a OrderWithCountDto
    public Task<List<(int ClientId, int Count)>> GetAfterDateAsync(DateTime fecha, CancellationToken ct = default) =>
        _db.Orders
            .Where(o => o.Orderdate > fecha)
            .GroupBy(o => o.Clientid)
            .Select(g => new ValueTuple<int, int>(g.Key, g.Count()))
            .ToListAsync(ct);

    // Incluimos Product y también el Client a través de Order
    public Task<List<Orderdetail>> GetAllDetailsAsync(CancellationToken ct = default) =>
        _db.Orderdetails
            .Include(od => od.Product)
            .Include(od => od.Order)
            .ThenInclude(o => o.Client)
            .ToListAsync(ct);

    // Ya devolvemos (NombreCliente, TotalPedidos)
    public async Task<(string Cliente, int TotalPedidos)?> GetTopClientAsync(CancellationToken ct = default)
    {
        var res = await _db.Orders
            .GroupBy(o => o.Clientid)
            .Select(g => new { ClientId = g.Key, Total = g.Count() })
            .Join(_db.Clients, g => g.ClientId, c => c.Clientid,
                (g, c) => new { c.Name, g.Total })
            .OrderByDescending(x => x.Total)
            .FirstOrDefaultAsync(ct);

        return res is null ? null : (res.Name, res.Total);
    }
}