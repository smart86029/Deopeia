namespace Deopeia.Identity.Application.Users.GetUser;

public record GetUserQuery(Guid Id) : IRequest<GetUserViewModel> { }
