using Deopeia.Identity.Domain.Roles;

namespace Deopeia.Identity.Infrastructure.Roles;

internal sealed class RoleLocaleConfiguration
    : EntityLocalizationConfiguration<Role, RoleLocalization, RoleCode>
{
    public override void Configure(EntityTypeBuilder<RoleLocalization> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.EntityId).HasColumnName(nameof(RoleCode).ToSnakeCaseLower());
    }
}
