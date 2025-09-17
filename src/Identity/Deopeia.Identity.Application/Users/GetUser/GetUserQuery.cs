namespace Deopeia.Identity.Application.Users.GetUser;

public sealed record GetUserQuery(Guid Id) : IQuery<GetUserResult>;
