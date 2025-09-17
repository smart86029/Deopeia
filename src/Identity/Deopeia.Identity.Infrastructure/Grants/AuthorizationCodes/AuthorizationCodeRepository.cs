using Deopeia.Identity.Domain.Grants.AuthorizationCodes;

namespace Deopeia.Identity.Infrastructure.Grants.AuthorizationCodes;

internal sealed class AuthorizationCodeRepository(IdentityContext context)
    : IAuthorizationCodeRepository
{
    private readonly DbSet<AuthorizationCode> _authorizationCodes =
        context.Set<AuthorizationCode>();

    public Task<AuthorizationCode?> GetAuthorizationCodeAsync(string code)
    {
        return _authorizationCodes.FirstOrDefaultAsync(x => x.Key == code);
    }

    public void Add(AuthorizationCode authorizationCode)
    {
        _authorizationCodes.Add(authorizationCode);
    }

    public void Remove(AuthorizationCode authorizationCode)
    {
        _authorizationCodes.Remove(authorizationCode);
    }
}
