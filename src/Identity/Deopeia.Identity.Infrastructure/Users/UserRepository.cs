using Deopeia.Identity.Domain.Users;

namespace Deopeia.Identity.Infrastructure.Users;

public sealed class UserRepository(IdentityContext context) : IUserRepository
{
    private readonly DbSet<User> _users = context.Set<User>();

    public Task<User> GetUserAsync(UserId userId)
    {
        return _users
            .Include(x => x.Authenticator)
            .Include(x => x.UserRoles)
            .Include(x => x.UserRefreshTokens)
            .FirstAsync(x => x.Id == userId);
    }

    public async Task<User?> GetUserAsync(string userName, string password)
    {
        var user = await _users
            .Include(x => x.Authenticator)
            .Include(x => x.UserRoles)
            .Include(x => x.UserRefreshTokens)
            .FirstOrDefaultAsync(x => x.UserName == userName);
        if (user is not null && user.PasswordHash != user.Hash(password))
        {
            return null;
        }

        return user;
    }

    public Task<bool> ContainsAsync(UserId userId)
    {
        return _users.AnyAsync(x => x.Id == userId);
    }

    public void Add(User user)
    {
        _users.Add(user);
    }

    public void Remove(User user)
    {
        _users.Remove(user);
    }
}
