using Deopeia.Identity.Domain.Grants.RefreshTokens;

namespace Deopeia.Identity.Infrastructure.Grants.RefreshTokens;

internal sealed class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder) { }
}
