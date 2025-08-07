namespace Deopeia.Identity.Api.Services;

public class UserService(ILogger<UserService> logger) : User.UserBase
{
    private readonly ILogger<UserService> _logger = logger;

    public override Task<GetUserResponse> GetUser(GetUserRequest request, ServerCallContext context)
    {
        return Task.FromResult(
            new GetUserResponse { Id = request.Id, Name = "User " + request.Id }
        );
    }
}
