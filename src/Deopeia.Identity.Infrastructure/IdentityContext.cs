using Deopeia.Identity.Domain.Clients;
using Deopeia.Identity.Domain.Grants;
using Deopeia.Identity.Domain.Permissions;
using Deopeia.Identity.Domain.Roles;
using Deopeia.Identity.Domain.Users;
using Deopeia.Identity.Infrastructure.Clients;
using Deopeia.Identity.Infrastructure.Grants;
using Deopeia.Identity.Infrastructure.Permissions;
using Deopeia.Identity.Infrastructure.Roles;
using Deopeia.Identity.Infrastructure.Users;

namespace Deopeia.Identity.Infrastructure;

public class IdentityContext(DbContextOptions<IdentityContext> options) : DbContext(options)
{
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.ApplyConventions();

        configurationBuilder.Properties<ClientId>().HaveConversion<ClientIdConverter>();
        configurationBuilder.Properties<GrantId>().HaveConversion<GrantIdConverter>();
        configurationBuilder.Properties<PermissionId>().HaveConversion<PermissionIdConverter>();
        configurationBuilder.Properties<RoleId>().HaveConversion<RoleIdConverter>();
        configurationBuilder.Properties<UserId>().HaveConversion<UserIdConverter>();
        configurationBuilder
            .Properties<UserRefreshTokenId>()
            .HaveConversion<UserRefreshTokenIdConverter>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(GetType().Assembly)
            .ApplyCommonConfigurations();
    }
}
