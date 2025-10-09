using Microsoft.EntityFrameworkCore;
using Lab_8___Carlos_Mamani.Models;


var builder = WebApplication.CreateBuilder(args);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext
builder.Services.AddDbContext<LINQExampleContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Lab8Connection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/clientes", async (string nombre, LINQExampleContext db) =>
    {
        var clientes = await db.Clients
            .Where(c => EF.Functions.ILike(c.Name!, $"%{nombre}%"))
            .ToListAsync();

        return clientes;
    })
    .WithName("GetClientesPorNombre")
    .WithOpenApi();

app.MapGet("/api/productos", async (decimal precio, LINQExampleContext db) =>
    {
        var productos = await db.Products
            .Where(p => p.Price > precio)
            .ToListAsync();

        return productos;
    })
    .WithName("GetProductosPorPrecio")
    .WithOpenApi();

app.MapGet("/api/ordenes/detalle", async (int orderId, LINQExampleContext db) =>
    {
        var detalles = await db.Orderdetails
            .Where(d => d.Orderid == orderId)
            .Select(d => new
            {
                NombreProducto = d.Product.Name,
                Cantidad = d.Quantity
            })
            .ToListAsync();

        return detalles;
    })
    .WithName("GetDetalleDeOrden")
    .WithOpenApi();

app.MapGet("/api/ordenes/totalproductos", async (int orderId, LINQExampleContext db) =>
    {
        var total = await db.Orderdetails
            .Where(d => d.Orderid == orderId)
            .Select(d => d.Quantity)
            .SumAsync();

        return new { OrderId = orderId, CantidadTotal = total };
    })
    .WithName("GetCantidadTotalPorOrden")
    .WithOpenApi();

app.MapGet("/api/productos/mascaro", async (LINQExampleContext db) =>
    {
        var productoMasCaro = await db.Products
            .OrderByDescending(p => p.Price)
            .FirstOrDefaultAsync();

        return productoMasCaro;
    })
    .WithName("GetProductoMasCaro")
    .WithOpenApi();

app.MapGet("/api/pedidos/fecha", async (DateTime fecha, LINQExampleContext db) =>
    {
        var pedidos = await db.Orders
            .Where(o => o.Orderdate > fecha)
            .ToListAsync();

        return pedidos;
    })
    .WithName("GetPedidosPorFecha")
    .WithOpenApi();

app.MapGet("/api/productos/promedio", async (LINQExampleContext db) =>
    {
        var promedio = await db.Products
            .Select(p => p.Price)
            .AverageAsync();

        return Results.Ok(new { PromedioPrecio = promedio });
    })
    .WithName("GetPromedioPrecioProductos")
    .WithOpenApi();

app.MapGet("/api/productos/sindescripcion", async (LINQExampleContext db) =>
    {
        var productosSinDescripcion = await db.Products
            .Where(p => p.Description == null || p.Description == "")
            .ToListAsync();

        return productosSinDescripcion;
    })
    .WithName("GetProductosSinDescripcion")
    .WithOpenApi();

app.Run();