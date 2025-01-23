using Deopeia.Identity.Domain.Roles;

namespace Deopeia.Identity.Infrastructure.Roles;

internal class RoleLocaleConfiguration : EntityLocaleConfiguration<Role, RoleLocale, RoleCode>
{
    public override void Configure(EntityTypeBuilder<RoleLocale> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.EntityId).HasColumnName(nameof(RoleCode).ToSnakeCaseLower());
    }
}
