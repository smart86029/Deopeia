namespace Deopeia.Identity.Domain.Grants.RefreshTokens;

public interface IRefreshTokenRepository : IRepository<RefreshToken, GrantId>
{
    Task<RefreshToken?> GetRefreshTokenAsync(string refreshToken);

    void Add(RefreshToken RefreshToken);

    void Update(RefreshToken RefreshToken);

    void Remove(RefreshToken RefreshToken);
}
