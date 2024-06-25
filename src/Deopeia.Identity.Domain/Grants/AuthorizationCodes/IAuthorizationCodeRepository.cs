namespace Deopeia.Identity.Domain.Grants.AuthorizationCodes;

public interface IAuthorizationCodeRepository : IRepository<AuthorizationCode>
{
    Task<AuthorizationCode?> GetAuthorizationCodeAsync(string code);

    void Add(AuthorizationCode authorizationCode);

    void Update(AuthorizationCode authorizationCode);

    void Remove(AuthorizationCode authorizationCode);
}
