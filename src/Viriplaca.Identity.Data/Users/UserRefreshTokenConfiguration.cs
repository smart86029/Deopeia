using Viriplaca.Identity.Domain.Users;

namespace Viriplaca.Identity.Data.Users;

internal class UserRefreshTokenConfiguration : EntityConfiguration<UserRefreshToken>
{
    public override void Configure(EntityTypeBuilder<UserRefreshToken> builder)
    {
        builder
            .Property(x => x.RefreshToken)
            .IsRequired()
            .HasColumnType("char(24)");

        builder
            .HasIndex(x => x.RefreshToken)
            .IsUnique();
    }
}
