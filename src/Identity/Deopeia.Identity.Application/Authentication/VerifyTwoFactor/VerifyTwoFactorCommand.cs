namespace Deopeia.Identity.Application.Authentication.VerifyTwoFactor;

public sealed record VerifyTwoFactorCommand(Guid UserId, string TwoFactorCode) : ICommand<bool>;
