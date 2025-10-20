using AutoMapper;                    // <- deja solo este using
// using AutoMapper.Extensions.Microsoft.DependencyInjection;  // <- QUITAR

using Lab_8___Carlos_Mamani.Mappings;
using Lab_8___Carlos_Mamani.Models;
using Lab_8___Carlos_Mamani.Repositories.Interfaces;
using Lab_8___Carlos_Mamani.Repositories.Implementations;
using Lab_8___Carlos_Mamani.Services.Interfaces;
using Lab_8___Carlos_Mamani.Services.Implementations;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<LINQExampleContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Lab8Connection")));

// AutoMapper (registro manual, compatible con v15)
var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<MappingProfile>();
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

// Repositories
builder.Services.AddScoped<IClientRepository,  ClientRepository>();
builder.Services.AddScoped<IOrderRepository,   OrderRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUnitOfWork,        UnitOfWork>();

// Services
builder.Services.AddScoped<IClientService,  ClientService>();
builder.Services.AddScoped<IOrderService,   OrderService>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment()) { app.UseSwagger(); app.UseSwaggerUI(); }
app.UseHttpsRedirection();
app.MapControllers();
app.Run();