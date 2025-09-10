namespace Deopeia.Identity.Application.Users.GetAuthenticator;

public sealed record GetAuthenticatorQuery(Guid UserId) : IQuery<GetAuthenticatorResult>;
