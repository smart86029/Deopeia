namespace Deopeia.Finance.Bff.Models.Identity;

public interface IIdentityApi
{
    [Get("/api/Users/{userId}/Authenticator")]
    Task<Authenticator> GetAuthenticatorAsync(Guid userId);

    [Put("/api/Users/{userId}/Authenticator")]
    Task EnableAuthenticator(Guid userId, [Body] EnableAuthenticatorCommand command);

    [Put("/api/Users/{userId}/Avatar")]
    Task UploadAvatar(Guid userId, [Body] UploadAvatarCommand command);

    [Put("/api/Users/{userId}/Password")]
    Task ChangePassword(Guid userId, [Body] ChangePasswordCommand command);
}
