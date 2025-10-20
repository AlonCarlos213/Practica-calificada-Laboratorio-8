using Lab_8___Carlos_Mamani.Models;

namespace Lab_8___Carlos_Mamani.Repositories.Interfaces;

public interface IClientRepository
{
    Task<List<Client>> GetByNameAsync(string nombre, CancellationToken ct = default);

    // Devuelve los Orderdetail del cliente (incluye Product y Order)
    Task<List<Orderdetail>> GetProductsByClientAsync(int clientId, CancellationToken ct = default);
}