using Lab_8___Carlos_Mamani.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab_8___Carlos_Mamani.Controllers;

[ApiController]
public class OrdersController : ControllerBase
{
    private readonly LINQExampleContext _db;
    public OrdersController(LINQExampleContext db) => _db = db;

    // GET /api/ordenes/detalle?orderId=1
    [HttpGet]
    [Route("api/ordenes/detalle")]
    public async Task<IActionResult> GetDetalleDeOrden([FromQuery] int orderId)
    {
        var detalles = await _db.Orderdetails
            .Where(d => d.Orderid == orderId)
            .Select(d => new
            {
                NombreProducto = d.Product.Name,
                Cantidad = d.Quantity
            })
            .ToListAsync();

        return Ok(detalles);
    }

    // GET /api/ordenes/totalproductos?orderId=1
    [HttpGet]
    [Route("api/ordenes/totalproductos")]
    public async Task<IActionResult> GetCantidadTotalPorOrden([FromQuery] int orderId)
    {
        var total = await _db.Orderdetails
            .Where(d => d.Orderid == orderId)
            .Select(d => d.Quantity)
            .SumAsync();

        return Ok(new { OrderId = orderId, CantidadTotal = total });
    }

    // GET /api/pedidos/fecha?fecha=2025-05-01
    [HttpGet]
    [Route("api/pedidos/fecha")]
    public async Task<IActionResult> GetPedidosPorFecha([FromQuery] DateTime fecha)
    {
        var pedidos = await _db.Orders
            .Where(o => o.Orderdate > fecha)
            .ToListAsync();

        return Ok(pedidos);
    }

    // GET /api/pedidos/detalles
    [HttpGet]
    [Route("api/pedidos/detalles")]
    public async Task<IActionResult> GetPedidosConDetalles()
    {
        var pedidosDetalles = await _db.Orderdetails
            .Select(od => new
            {
                od.Orderid,
                Producto = od.Product.Name,
                od.Quantity
            })
            .ToListAsync();

        return Ok(pedidosDetalles);
    }
}