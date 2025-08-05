namespace Deopeia.Identity.Domain.Users;

public interface IUserRepository : IRepository<User, UserId>
{
    Task<User> GetUserAsync(UserId userId);

    Task<User?> GetUserAsync(string userName, string password);

    Task<bool> ContainsAsync(UserId userId);

    void Add(User user);

    void Remove(User user);
}
