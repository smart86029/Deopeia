using Deopeia.Identity.Domain.Clients;

namespace Deopeia.Identity.Infrastructure.Clients;

internal sealed class ClientRepository(IdentityContext context) : IClientRepository
{
    private readonly DbSet<Client> _clients = context.Set<Client>();

    public Task<ICollection<Client>> GetClientsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Client> GetClientAsync(ClientId clientId)
    {
        return _clients.FirstAsync(x => x.Id == clientId);
    }

    public Task<Client?> GetClientAsync(string name)
    {
        return _clients.FirstOrDefaultAsync(x => x.Name == name);
    }
}
