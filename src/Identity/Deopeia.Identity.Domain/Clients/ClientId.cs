namespace Deopeia.Identity.Domain.Clients;

public readonly record struct ClientId(Guid Guid) : IEntityId
{
    public ClientId()
        : this(Guid.CreateVersion7()) { }
}
