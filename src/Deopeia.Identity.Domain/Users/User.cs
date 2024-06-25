using System.Security.Cryptography;
using Deopeia.Identity.Domain.Roles;

namespace Deopeia.Identity.Domain.Users;

public class User : AggregateRoot
{
    private readonly List<UserRole> _userRoles = [];
    private readonly List<UserRefreshToken> _userRefreshTokens = [];

    private User() { }

    public User(string userName, string password, bool isEnabled)
    {
        userName.MustNotBeNullOrWhiteSpace();
        password.MustNotBeNullOrWhiteSpace();

        UserName = userName.ToLower().Trim();
        UpdateSalt();
        PasswordHash = Hash(password);
        IsEnabled = isEnabled;
        AddDomainEvent(new UserCreated(Id, UserName));
    }

    public string UserName { get; private init; } = string.Empty;

    public string Salt { get; private set; } = string.Empty;

    public string PasswordHash { get; private set; } = string.Empty;

    public bool IsEnabled { get; private set; }

    public DateTimeOffset CreatedAt { get; private init; } = DateTimeOffset.UtcNow;

    public IReadOnlyCollection<UserRole> UserRoles => _userRoles.AsReadOnly();

    public IReadOnlyCollection<UserRefreshToken> UserRefreshTokens =>
        _userRefreshTokens.AsReadOnly();

    public void UpdatePassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            return;
        }

        UpdateSalt();
        PasswordHash = Hash(password);
    }

    public void Enable()
    {
        IsEnabled = true;
        AddDomainEvent(new UserEnabled(Id));
    }

    public void Disable()
    {
        IsEnabled = false;
        AddDomainEvent(new UserDisabled(Id));
    }

    public void AssignRole(Role role)
    {
        if (_userRoles.Any(x => x.RoleId == role.Id))
        {
            return;
        }

        _userRoles.Add(new UserRole(Id, role.Id));
    }

    public void UnassignRole(Role role)
    {
        var userRole = _userRoles.FirstOrDefault(x => x.RoleId == role.Id);
        if (userRole is null)
        {
            return;
        }

        _userRoles.Remove(userRole);
    }

    public bool IsValidRefreshToken(string refreshToken)
    {
        return _userRefreshTokens.Any(x => x.RefreshToken == refreshToken && x.IsActive);
    }

    public void AddRefreshToken(string refreshToken, TimeSpan expiry)
    {
        var token = new UserRefreshToken(refreshToken, DateTimeOffset.UtcNow.Add(expiry), Id);
        _userRefreshTokens.Add(token);
    }

    public void RemoveRefreshToken(string refreshToken)
    {
        var token = _userRefreshTokens.First(t => t.RefreshToken == refreshToken);
        _userRefreshTokens.Remove(token);
    }

    public string Hash(string password)
    {
        return (password.Trim() + Salt).Sha256();
    }

    private void UpdateSalt()
    {
        Salt = RandomNumberGenerator.GetBytes(32).ToBase64String();
    }
}
