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
        return await _clients.FirstAsync(x => x.Id == clientId);
    }

    public async Task<Client?> GetClientAsync(string name)
    {
        return await _clients.FirstOrDefaultAsync(x => x.Name == name);
    }
}
