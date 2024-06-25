using Deopeia.Identity.Domain.Grants.RefreshTokens;

namespace Deopeia.Identity.Infrastructure.Grants.AuthorizationCodes;

internal class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder) { }
}
