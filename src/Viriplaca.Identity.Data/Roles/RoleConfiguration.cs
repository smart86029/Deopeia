using Viriplaca.Identity.Domain.Roles;

namespace Viriplaca.Identity.Data.Roles;

public class RoleConfiguration : EntityConfiguration<Role>
{
    public override void Configure(EntityTypeBuilder<Role> builder)
    {
        builder
            .Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(32);

        builder.Metadata
            .FindNavigation(nameof(Role.UserRoles))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Metadata
            .FindNavigation(nameof(Role.RolePermissions))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
