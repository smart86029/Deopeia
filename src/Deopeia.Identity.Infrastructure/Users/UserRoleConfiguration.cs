using Deopeia.Identity.Domain.Users;

namespace Deopeia.Identity.Infrastructure.Users;

internal class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.HasKey(x => new { x.UserId, x.RoleId });
    }
}
