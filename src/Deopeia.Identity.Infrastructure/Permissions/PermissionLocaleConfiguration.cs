using Deopeia.Identity.Domain.Permissions;

namespace Deopeia.Identity.Infrastructure.Permissions;

internal class PermissionLocaleConfiguration
    : EntityLocaleConfiguration<Permission, PermissionLocale, PermissionCode>
{
    public override void Configure(EntityTypeBuilder<PermissionLocale> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.EntityId).HasColumnName(nameof(PermissionCode).ToSnakeCaseLower());
    }
}
