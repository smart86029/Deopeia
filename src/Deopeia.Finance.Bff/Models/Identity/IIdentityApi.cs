namespace Deopeia.Finance.Bff.Models.Identity;

public interface IIdentityApi
{
    [Get("/api/Users/{userId}/Authenticator")]
    Task<Authenticator> GetContractsAsync(Guid userId);
}
