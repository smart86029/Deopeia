using Deopeia.Identity.Domain.Clients;

namespace Deopeia.Identity.Infrastructure.Clients;

internal class ClientIdConverter()
    : ValueConverter<ClientId, Guid>(id => id.Guid, guid => new ClientId(guid)) { }
