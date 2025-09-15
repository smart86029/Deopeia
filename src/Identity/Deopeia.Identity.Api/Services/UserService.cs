using Deopeia.Identity.Application.Users.ChangePassword;
using Deopeia.Identity.Application.Users.CreateUser;
using Deopeia.Identity.Application.Users.EnableAuthenticator;
using Deopeia.Identity.Application.Users.GetAuthenticator;
using Deopeia.Identity.Application.Users.GetAvatar;
using Deopeia.Identity.Application.Users.GetUser;
using Deopeia.Identity.Application.Users.GetUsers;
using Deopeia.Identity.Application.Users.UpdateUser;
using Deopeia.Identity.Application.Users.UploadAvatar;
using Deopeia.Identity.Contracts;

namespace Deopeia.Identity.Api.Services;

public class UserService(IMediator mediator) : Contracts.UserService.UserServiceBase
{
    private readonly IMediator _mediator = mediator;

    public override async Task<ListUserResponse> ListUser(
        ListUserRequest request,
        ServerCallContext context
    )
    {
        var query = request.Adapt<GetUsersQuery>();
        var users = await _mediator.Send(query);
        return users.Adapt<ListUserResponse>();
    }

    public override async Task<GetUserResponse> GetUser(
        GetUserRequest request,
        ServerCallContext context
    )
    {
        var query = new GetUserQuery(request.Id);
        var user = await _mediator.Send(query);
        return user.Adapt<GetUserResponse>();
    }

    public override async Task<Empty> CreateUser(
        CreateUserRequest request,
        ServerCallContext context
    )
    {
        var command = request.Adapt<CreateUserCommand>();
        await _mediator.Send(command);
        return new Empty();
    }

    public override async Task<Empty> UpdateUser(
        UpdateUserRequest request,
        ServerCallContext context
    )
    {
        var command = request.Adapt<UpdateUserCommand>();
        await _mediator.Send(command);
        return new Empty();
    }

    public override async Task<GetAuthenticatorResponse> GetAuthenticator(
        GetAuthenticatorRequest request,
        ServerCallContext context
    )
    {
        var query = request.Adapt<GetAuthenticatorQuery>();
        var response = await _mediator.Send(query);
        return response.Adapt<GetAuthenticatorResponse>();
    }

    public override async Task<Empty> EnableAuthenticator(
        EnableAuthenticatorRequest request,
        ServerCallContext context
    )
    {
        var command = request.Adapt<EnableAuthenticatorCommand>();
        await _mediator.Send(command);
        return new Empty();
    }

    public override async Task<GetAvatarResponse> GetAvatar(
        GetAvatarRequest request,
        ServerCallContext context
    )
    {
        var query = new GetAvatarQuery(request.UserId);
        var uri = await _mediator.Send(query);
        return new GetAvatarResponse { Url = uri?.ToString() };
    }

    public override async Task<Empty> UploadAvatar(
        IAsyncStreamReader<UploadAvatarRequest> requestStream,
        ServerCallContext context
    )
    {
        var userId = Guid.Empty;
        var fileName = string.Empty;
        using var memoryStream = new MemoryStream();

        await foreach (var request in requestStream.ReadAllAsync())
        {
            if (request.UserId is not null)
            {
                userId = (Guid)request.UserId;
            }

            if (!request.FileName.IsNullOrWhiteSpace())
            {
                fileName = request.FileName;
            }

            if (request.Content is not null)
            {
                await memoryStream.WriteAsync(request.Content.Memory);
            }
        }

        var command = new UploadAvatarCommand(userId, fileName, memoryStream.ToArray());
        await _mediator.Send(command);
        return new Empty();
    }

    public override async Task<Empty> ChangePassword(
        ChangePasswordRequest request,
        ServerCallContext context
    )
    {
        var command = request.Adapt<ChangePasswordCommand>();
        await _mediator.Send(command);
        return new Empty();
    }
}
