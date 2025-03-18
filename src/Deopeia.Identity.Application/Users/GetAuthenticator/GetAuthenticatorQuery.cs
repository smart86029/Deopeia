namespace Deopeia.Identity.Application.Users.GetAuthenticator;

public record GetAuthenticatorQuery(Guid UserId) : IRequest<GetAuthenticatorResult> { }
