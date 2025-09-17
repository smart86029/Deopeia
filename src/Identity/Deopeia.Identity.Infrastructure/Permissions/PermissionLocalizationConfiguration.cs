using Deopeia.Identity.Domain.Permissions;

namespace Deopeia.Identity.Infrastructure.Permissions;

internal class PermissionLocalizationConfiguration
    : EntityLocalizationConfiguration<Permission, PermissionLocalization, PermissionCode>
{
    public override void Configure(EntityTypeBuilder<PermissionLocalization> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.EntityId).HasColumnName(nameof(PermissionCode).ToSnakeCaseLower());
    }
}
