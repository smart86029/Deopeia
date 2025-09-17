using Deopeia.Identity.Domain.Grants.RefreshTokens;

namespace Deopeia.Identity.Infrastructure.Grants.RefreshTokens;

internal sealed class RefreshTokenRepository(IdentityContext context) : IRefreshTokenRepository
{
    private readonly DbSet<RefreshToken> _refreshTokens = context.Set<RefreshToken>();

    public Task<RefreshToken?> GetRefreshTokenAsync(string refreshToken)
    {
        return _refreshTokens.FirstOrDefaultAsync(x => x.Key == refreshToken);
    }

    public void Add(RefreshToken refreshToken)
    {
        _refreshTokens.Add(refreshToken);
    }

    public void Update(RefreshToken refreshToken)
    {
        _refreshTokens.Update(refreshToken);
    }

    public void Remove(RefreshToken refreshToken)
    {
        _refreshTokens.Remove(refreshToken);
    }
}
