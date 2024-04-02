namespace Viriplaca.Identity.Domain.Clients;

public interface IClientRepository : IRepository<Client>
{
    public Task<ICollection<Client>> GetClientsAsync();

    public Task<Client> GetClientAsync(Guid clientId);

    public Task<Client> GetClientAsync(string name);
}
