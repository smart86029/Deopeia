using Deopeia.Identity.Application.Users.GetUser;
using Deopeia.Identity.Application.Users.GetUsers;

namespace Deopeia.Identity.Api.Services;

public class UserService(IMediator mediator) : User.UserBase
{
    private readonly IMediator _mediator = mediator;

    public override async Task<ListUserResponse> ListUser(
        ListUserRequest request,
        ServerCallContext context
    )
    {
        var query = new GetUsersQuery(request.UserName, request.IsEnabled, request.RoleCode)
        {
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
        };
        var users = await _mediator.Send(query);
        return new ListUserResponse
        {
            PageIndex = users.PageIndex,
            PageSize = users.PageSize,
            TotalCount = users.TotalCount,
            Users =
            {
                users.Items.Select(u => new ListUserResponse.Types.User
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    IsEnabled = u.IsEnabled,
                    RoleCodes = { u.RoleCodes },
                }),
            },
        };
    }

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
