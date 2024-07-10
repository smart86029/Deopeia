using Deopeia.Identity.Domain.Users;

namespace Deopeia.Identity.Infrastructure.Users;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(x => x.UserName).IsUnique();
    }
}
