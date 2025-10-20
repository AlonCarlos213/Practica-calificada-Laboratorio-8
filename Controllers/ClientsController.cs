using Lab_8___Carlos_Mamani.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab_8___Carlos_Mamani.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly LINQExampleContext _db;
    public ClientesController(LINQExampleContext db) => _db = db;

    // GET /api/clientes?nombre=...
    [HttpGet]
    public async Task<IActionResult> GetClientes([FromQuery] string nombre)
    {
        var clientes = await _db.Clients
            .Where(c => EF.Functions.ILike(c.Name!, $"%{nombre}%"))
            .ToListAsync();

        return Ok(clientes);
    }

    // GET /api/clientes/mayorpedidos
    [HttpGet("mayorpedidos")]
    public async Task<IActionResult> GetClienteConMasPedidos()
    {
        var clienteConMasPedidos = await _db.Orders
            .GroupBy(o => o.Clientid)
            .Select(g => new
            {
                ClientId = g.Key,
                CantidadPedidos = g.Count()
            })
            .OrderByDescending(c => c.CantidadPedidos)
            .FirstOrDefaultAsync();

        if (clienteConMasPedidos is null) return NotFound();

        var cliente = await _db.Clients
            .Where(c => c.Clientid == clienteConMasPedidos.ClientId)
            .Select(c => c.Name)
            .FirstOrDefaultAsync();

        return Ok(new
        {
            Cliente = cliente,
            TotalPedidos = clienteConMasPedidos.CantidadPedidos
        });
    }

    // GET /api/clientes/{clientid}/productos
    [HttpGet("{clientid:int}/productos")]
    public async Task<IActionResult> GetProductosPorCliente([FromRoute] int clientid)
    {
        var productosCliente = await _db.Orderdetails
            .Where(od => od.Order.Clientid == clientid)
            .Select(od => new
            {
                Cliente = od.Order.Client.Name,
                Producto = od.Product.Name,
                Cantidad = od.Quantity
            })
            .ToListAsync();

        if (!productosCliente.Any())
            return NotFound($"No se encontraron productos para el cliente con ID {clientid}.");

        return Ok(productosCliente);
    }
}
