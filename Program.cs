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

app.Run();