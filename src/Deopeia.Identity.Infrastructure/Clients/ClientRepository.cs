using Deopeia.Identity.Domain.Clients;

namespace Deopeia.Identity.Infrastructure.Clients;

internal class ClientRepository(IdentityContext context) : IClientRepository
{
    private readonly DbSet<Client> _clients = context.Set<Client>();

    public Task<ICollection<Client>> GetClientsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Client> GetClientAsync(ClientId clientId)
    {
        var result = await _clients.FirstAsync(x => x.Id == clientId);

        return result;
    }

    public async Task<Client> GetClientAsync(string name)
    {
        var result = await _clients.FirstAsync(x => x.Name == name);

        return result;
    }
}
