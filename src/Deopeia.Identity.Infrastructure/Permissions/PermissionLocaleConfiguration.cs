using Deopeia.Identity.Domain.Permissions;

namespace Deopeia.Identity.Infrastructure.Permissions;

internal class PermissionLocaleConfiguration
    : EntityLocaleConfiguration<Permission, PermissionLocale, PermissionId>
{
    public override void Configure(EntityTypeBuilder<PermissionLocale> builder)
    {
        base.Configure(builder);
    }
}
