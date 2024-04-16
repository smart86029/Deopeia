using Viriplaca.Identity.Domain.Grants.AuthorizationCodes;

namespace Viriplaca.Identity.Data.Grants.AuthorizationCodes;

internal class AuthorizationCodeRepository(IdentityContext context)
    : IAuthorizationCodeRepository
{
    private readonly DbSet<AuthorizationCode> _authorizationCodes = context.Set<AuthorizationCode>();

    public Task<AuthorizationCode?> GetAuthorizationCodeAsync(string code)
    {
        var result = _authorizationCodes.FirstOrDefaultAsync(x => x.Key == code);

        return result;
    }

    public void Add(AuthorizationCode authorizationCode)
    {
        _authorizationCodes.Add(authorizationCode);
    }

    public void Update(AuthorizationCode authorizationCode)
    {
        _authorizationCodes.Update(authorizationCode);
    }

    public void Remove(AuthorizationCode authorizationCode)
    {
        _authorizationCodes.Remove(authorizationCode);
    }
}
