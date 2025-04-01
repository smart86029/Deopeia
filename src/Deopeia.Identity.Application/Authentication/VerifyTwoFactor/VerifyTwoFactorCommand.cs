namespace Deopeia.Identity.Application.Authentication.VerifyTwoFactor;

public record VerifyTwoFactorCommand(Guid UserId, string TwoFactorCode) : IRequest<bool> { }
