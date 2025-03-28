namespace Deopeia.Finance.Bff.Models.Identity;

public interface IIdentityApi
{
    [Get("/api/Users/{userId}/Authenticator")]
    Task<Authenticator> GetAuthenticatorAsync(Guid userId);

    [Put("/api/Users/{userId}/Authenticator")]
    Task EnableAuthenticator(Guid userId, [Body] EnableAuthenticatorCommand command);
}
