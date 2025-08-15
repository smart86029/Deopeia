using Deopeia.Identity.Domain.Grants;

namespace Deopeia.Identity.Infrastructure.Grants;

internal class GrantIdConverter()
    : ValueConverter<GrantId, Guid>(id => id.Guid, guid => new GrantId(guid)) { }
