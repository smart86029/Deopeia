using Viriplaca.Identity.Domain.Grants.RefreshTokens;

namespace Viriplaca.Identity.Data.Grants.RefreshTokens;

internal class RefreshTokenRepository(IdentityContext context)
    : IRefreshTokenRepository
{
    private readonly DbSet<RefreshToken> _refreshTokens = context.Set<RefreshToken>();

    public Task<RefreshToken?> GetRefreshTokenAsync(string refreshToken)
    {
        var result = _refreshTokens.FirstOrDefaultAsync(x => x.Key == refreshToken);

        return result;
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
