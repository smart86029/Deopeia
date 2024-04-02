using Viriplaca.Identity.Domain.Users;

namespace Viriplaca.Identity.Data.Users;

internal class UserConfiguration : EntityConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .Property(x => x.UserName)
            .IsRequired()
            .HasMaxLength(32);

        builder
            .HasIndex(x => x.UserName)
            .IsUnique();

        builder
            .Property(x => x.Salt)
            .IsRequired()
            .HasMaxLength(64);

        builder
            .Property(x => x.PasswordHash)
            .IsRequired()
            .HasMaxLength(256);
    }
}
