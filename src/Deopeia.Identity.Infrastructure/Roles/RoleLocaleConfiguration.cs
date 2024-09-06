using Deopeia.Identity.Domain.Roles;

namespace Deopeia.Identity.Infrastructure.Roles;

internal class RoleLocaleConfiguration : EntityLocaleConfiguration<Role, RoleLocale, RoleId>
{
    public override void Configure(EntityTypeBuilder<RoleLocale> builder)
    {
        base.Configure(builder);
    }
}
