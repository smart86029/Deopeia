using Deopeia.Identity.Domain.Roles;

namespace Deopeia.Identity.Infrastructure.Roles;

internal class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.Property(x => x.Id).HasColumnName("code");

        builder
            .Metadata.FindNavigation(nameof(Role.UserRoles))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder
            .Metadata.FindNavigation(nameof(Role.RolePermissions))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
