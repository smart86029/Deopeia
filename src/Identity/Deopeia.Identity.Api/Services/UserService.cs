using Deopeia.Identity.Application.Users.GetUser;

namespace Deopeia.Identity.Api.Services;

public class UserService(IMediator mediator) : User.UserBase
{
    private readonly IMediator _mediator = mediator;

    public override async Task<GetUserResponse> GetUser(
        GetUserRequest request,
        ServerCallContext context
    )
    {
        var query = new GetUserQuery(request.Id);
        var user = await _mediator.Send(query);
        return new GetUserResponse { Id = user.Id, UserName = user.UserName };
    }
}
