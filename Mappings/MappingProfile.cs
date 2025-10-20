using AutoMapper;
using Lab_8___Carlos_Mamani.DTOs;
using Lab_8___Carlos_Mamani.Models;

namespace Lab_8___Carlos_Mamani.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Client -> ClientDto
        CreateMap<Client, ClientDto>()
            .ForMember(d => d.ClientId, m => m.MapFrom(s => s.Clientid));

        // Product -> ProductDto
        CreateMap<Product, ProductDto>()
            .ForMember(d => d.ProductId, m => m.MapFrom(s => s.Productid));

        // Orderdetail -> OrderDetailDto (incluye navegaciones)
        CreateMap<Orderdetail, OrderDetailDto>()
            .ForMember(d => d.OrderId,     m => m.MapFrom(s => s.Orderid))
            .ForMember(d => d.ProductId,   m => m.MapFrom(s => s.Productid))
            .ForMember(d => d.Quantity,    m => m.MapFrom(s => s.Quantity))
            .ForMember(d => d.ProductName, m => m.MapFrom(s => s.Product != null ? s.Product.Name : null))
            .ForMember(d => d.ClientName,  m => m.MapFrom(s => s.Order != null && s.Order.Client != null ? s.Order.Client.Name : null))
            .ForMember(d => d.OrderDate,   m => m.MapFrom(s => s.Order != null ? s.Order.Orderdate : (DateTime?)null));

        // (ClientId, Count) -> OrderWithCountDto
        CreateMap<(int ClientId, int Count), OrderWithCountDto>()
            .ForMember(d => d.ClientId, m => m.MapFrom(s => s.ClientId))
            .ForMember(d => d.Count,    m => m.MapFrom(s => s.Count));
    }
}