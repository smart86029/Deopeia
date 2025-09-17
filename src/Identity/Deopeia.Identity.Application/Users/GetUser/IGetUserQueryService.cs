namespace Deopeia.Identity.Application.Users.GetUser;

public interface IGetUserQueryService
{
    Task<GetUserResult> GetAsync(GetUserQuery query);
}
