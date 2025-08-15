using Deopeia.Identity.Domain.Roles;

namespace Deopeia.Identity.Infrastructure.Roles;

internal class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.HasKey(x => new { x.RoleCode, x.PermissionCode });
    }
}
