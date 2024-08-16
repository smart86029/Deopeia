namespace Deopeia.Identity.Domain.Clients;

public interface IClientRepository : IRepository<Client, ClientId>
{
    public Task<ICollection<Client>> GetClientsAsync();

    public Task<Client> GetClientAsync(ClientId clientId);

    public Task<Client> GetClientAsync(string name);
}
