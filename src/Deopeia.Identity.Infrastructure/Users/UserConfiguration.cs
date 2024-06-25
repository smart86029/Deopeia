using Deopeia.Identity.Domain.Users;

namespace Deopeia.Identity.Infrastructure.Users;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.UserName).IsRequired().HasMaxLength(32);

        builder.HasIndex(x => x.UserName).IsUnique();

        builder.Property(x => x.Salt).IsRequired().HasMaxLength(64);

        builder.Property(x => x.PasswordHash).IsRequired().HasMaxLength(256);
    }
}
