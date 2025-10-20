using AutoMapper;
using Lab_8___Carlos_Mamani.DTOs;
using Lab_8___Carlos_Mamani.Services.Interfaces;
using Lab_8___Carlos_Mamani.Repositories.Interfaces;

namespace Lab_8___Carlos_Mamani.Services.Implementations;

public class ClientService : IClientService
{
    private readonly IClientRepository _clients;
    private readonly IMapper _mapper;

    public ClientService(IClientRepository clients, IMapper mapper)
    {
        _clients = clients;
        _mapper = mapper;
    }

    public async Task<List<ClientDto>> GetByNameAsync(string name)
    {
        var entities = await _clients.GetByNameAsync(name);            // <-- método del repo
        return _mapper.Map<List<ClientDto>>(entities);
    }

    public async Task<object> GetProductsByClientAsync(int clientId)
    {
        // Puedes crear un DTO propio; dejo object para respetar tu interfaz.
        var rows = await _clients.GetProductsByClientAsync(clientId);   // <-- método del repo
        return rows;
    }
}