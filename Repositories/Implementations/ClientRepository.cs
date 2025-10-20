using Lab_8___Carlos_Mamani.Models;
using Lab_8___Carlos_Mamani.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Lab_8___Carlos_Mamani.DTOs;

namespace Lab_8___Carlos_Mamani.Repositories.Implementations;

public class ClientRepository : IClientRepository
{
    private readonly LINQExampleContext _db;
    public ClientRepository(LINQExampleContext db) => _db = db;

    // 🟢 Paso 2: Consulta básica con AsNoTracking()
    // Busca clientes cuyo nombre contenga el texto indicado
    public Task<List<Client>> GetByNameAsync(string nombre, CancellationToken ct = default) =>
        _db.Clients
            .AsNoTracking() // mejora rendimiento al no rastrear entidades
            .Where(c => EF.Functions.ILike(c.Name!, $"%{nombre}%"))
            .ToListAsync(ct);

    // 🟢 Paso 3: Consulta con Include() y ThenInclude() para referencias
    // Obtiene los productos asociados a un cliente (pedidos + productos)
    public Task<List<Orderdetail>> GetProductsByClientAsync(int clientId, CancellationToken ct = default) =>
        _db.Orderdetails
            .Include(od => od.Product)                 // relación Orderdetail → Product
            .Include(od => od.Order)                   // relación Orderdetail → Order
                .ThenInclude(o => o.Client)            // relación Order → Client
            .AsNoTracking()                            // lectura sin tracking
            .Where(od => od.Order.Clientid == clientId)
            .ToListAsync(ct);

    // 🟢 Paso 4: Doble consulta (uso de SelectMany y Sum)
    // Calcula el total de productos comprados por cada cliente
    public async Task<List<ClientProductCountDto>> GetClientsWithProductCountAsync(CancellationToken ct = default)
    {
        return await _db.Clients
            .AsNoTracking()
            .Select(client => new ClientProductCountDto
            {
                ClientName = client.Name,
                TotalProducts = client.Orders
                    .SelectMany(order => order.Orderdetails)
                    .Sum(detail => detail.Quantity)
            })
            .ToListAsync(ct);
    }
    public async Task<List<SalesByClientDto>> GetSalesByClientAsync(CancellationToken ct = default)
    {
        return await _db.Orders
            .Include(order => order.Orderdetails)
            .ThenInclude(detail => detail.Product)
            .AsNoTracking()
            .GroupBy(order => order.Clientid)
            .Select(group => new SalesByClientDto
            {
                ClientName = _db.Clients
                    .Where(c => c.Clientid == group.Key)
                    .Select(c => c.Name)
                    .FirstOrDefault()!,
                TotalSales = group.Sum(order =>
                    order.Orderdetails.Sum(detail => detail.Quantity * detail.Product.Price))
            })
            .OrderByDescending(s => s.TotalSales)
            .ToListAsync(ct);
    }
}
