namespace Deopeia.Identity.Domain.Grants.AuthorizationCodes;

public interface IAuthorizationCodeRepository : IRepository<AuthorizationCode, GrantId>
{
    Task<AuthorizationCode?> GetAuthorizationCodeAsync(string code);

    void Add(AuthorizationCode authorizationCode);

    void Remove(AuthorizationCode authorizationCode);
}
