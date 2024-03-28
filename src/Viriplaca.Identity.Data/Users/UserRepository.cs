using Viriplaca.Common.Utilities;
using Viriplaca.Identity.Domain.Users;

namespace Viriplaca.Identity.Data.Users;

public class UserRepository(IdentityContext context)
    : IUserRepository
{
    private readonly DbSet<User> _users = context.Set<User>();

    public async Task<User> GetUserAsync(Guid userId)
    {
        var result = await _users
            .Include(x => x.UserRoles)
            .Include(x => x.UserRefreshTokens)
            .FirstAsync(x => x.Id == userId);

        return result;
    }

    public async Task<User> GetUserAsync(string userName, string password)
    {
        var result = await _users
            .Include(x => x.UserRoles)
            .Include(x => x.UserRefreshTokens)
            .FirstAsync(x => x.UserName == userName);
        if (result.PasswordHash != CryptographyUtility.SHA256Hash(password.Trim(), result.Salt))
        {
            throw new Exception("Not found.");
        }

        return result;
    }

    public async Task<bool> ContainsAsync(Guid userId)
    {
        var result = await _users
            .AnyAsync(x => x.Id == userId);

        return result;
    }

    public void Add(User user)
    {
        _users.Add(user);
    }

    public void Update(User user)
    {
        _users.Update(user);
    }

    public void Remove(User user)
    {
        _users.Remove(user);
    }
}
