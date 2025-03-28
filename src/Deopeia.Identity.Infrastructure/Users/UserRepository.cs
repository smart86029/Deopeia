using Deopeia.Identity.Domain.Users;

namespace Deopeia.Identity.Infrastructure.Users;

public class UserRepository(IdentityContext context) : IUserRepository
{
    private readonly DbSet<User> _users = context.Set<User>();

    public async Task<User> GetUserAsync(UserId userId)
    {
        return await _users
            .Include(x => x.Authenticator)
            .Include(x => x.UserRoles)
            .Include(x => x.UserRefreshTokens)
            .FirstAsync(x => x.Id == userId);
    }

    public async Task<User?> GetUserAsync(string userName, string password)
    {
        var result = await _users
            .Include(x => x.Authenticator)
            .Include(x => x.UserRoles)
            .Include(x => x.UserRefreshTokens)
            .FirstOrDefaultAsync(x => x.UserName == userName);
        if (result is not null && result.PasswordHash != result.Hash(password))
        {
            return null;
        }

        return result;
    }

    public async Task<bool> ContainsAsync(UserId userId)
    {
        var result = await _users.AnyAsync(x => x.Id == userId);

        return result;
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
