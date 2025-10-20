using Lab_8___Carlos_Mamani.DTOs;

namespace Lab_8___Carlos_Mamani.Services.Interfaces;

public interface IClientService
{
    Task<List<ClientDto>> GetByNameAsync(string name);
    Task<object> GetProductsByClientAsync(int clientId); // puedes tiparlo con un DTO específico si quieres
}