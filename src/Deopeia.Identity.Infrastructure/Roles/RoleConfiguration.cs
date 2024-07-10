using Deopeia.Identity.Domain.Roles;

namespace Deopeia.Identity.Infrastructure.Roles;

internal class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder
            .Metadata.FindNavigation(nameof(Role.UserRoles))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder
            .Metadata.FindNavigation(nameof(Role.RolePermissions))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
