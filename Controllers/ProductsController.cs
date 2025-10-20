using Lab_8___Carlos_Mamani.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab_8___Carlos_Mamani.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductosController : ControllerBase
{
    private readonly LINQExampleContext _db;
    public ProductosController(LINQExampleContext db) => _db = db;

    // GET /api/productos?precio=...
    [HttpGet]
    public async Task<IActionResult> GetProductos([FromQuery] decimal precio)
    {
        var productos = await _db.Products
            .Where(p => p.Price > precio)
            .ToListAsync();

        return Ok(productos);
    }

    // GET /api/productos/mascaro
    [HttpGet("mascaro")]
    public async Task<IActionResult> GetProductoMasCaro()
    {
        var producto = await _db.Products
            .OrderByDescending(p => p.Price)
            .FirstOrDefaultAsync();

        return producto is null ? NotFound() : Ok(producto);
    }

    // GET /api/productos/promedio
    [HttpGet("promedio")]
    public async Task<IActionResult> GetPromedioPrecio()
    {
        var promedio = await _db.Products.Select(p => p.Price).AverageAsync();
        return Ok(new { PromedioPrecio = promedio });
    }

    // GET /api/productos/sindescripcion
    [HttpGet("sindescripcion")]
    public async Task<IActionResult> GetProductosSinDescripcion()
    {
        var productos = await _db.Products
            .Where(p => p.Description == null || p.Description == "")
            .ToListAsync();

        return Ok(productos);
    }

    // GET /api/productos/{productid}/clientes
    [HttpGet("{productid:int}/clientes")]
    public async Task<IActionResult> GetClientesPorProducto([FromRoute] int productid)
    {
        var clientesPorProducto = await _db.Orderdetails
            .Where(od => od.Productid == productid)
            .Select(od => new
            {
                Producto = od.Product.Name,
                Cliente = od.Order.Client.Name,
                FechaPedido = od.Order.Orderdate
            })
            .Distinct()
            .ToListAsync();

        if (!clientesPorProducto.Any())
            return NotFound($"No se encontraron clientes para el producto con ID {productid}.");

        return Ok(clientesPorProducto);
    }
}
