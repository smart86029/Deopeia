namespace Deopeia.Identity.Application.Users.GetUser;

public interface IGetUserQueryService
{
    Task<GetUserViewModel> GetAsync(GetUserQuery query);
}
