using Deopeia.Identity.Domain.Roles;

namespace Deopeia.Identity.Domain.Users;

public class User : AggregateRoot<UserId>
{
    private const int SaltLength = 32;

    private readonly List<UserRole> _userRoles = [];
    private readonly List<UserRefreshToken> _userRefreshTokens = [];

    private User()
    {
        Authenticator = new(Id);
    }

    public User(string userName, string password, bool isEnabled)
    {
        userName.MustNotBeNullOrWhiteSpace();
        password.MustNotBeNullOrWhiteSpace();

        UserName = userName.ToLower().Trim();
        UpdateSalt();
        PasswordHash = Hash(password);
        IsEnabled = isEnabled;
        Authenticator = new(Id);
    }

    public string UserName { get; private init; } = string.Empty;

    public string Salt { get; private set; } = string.Empty;

    public string PasswordHash { get; private set; } = string.Empty;

    public bool IsEnabled { get; private set; }

    public FileResourceId? AvatarId { get; private set; }

    public DateTimeOffset CreatedAt { get; private init; } = DateTimeOffset.UtcNow;

    public Authenticator Authenticator { get; private init; }

    public IReadOnlyCollection<UserRole> UserRoles => _userRoles.AsReadOnly();

    public IReadOnlyCollection<UserRefreshToken> UserRefreshTokens =>
        _userRefreshTokens.AsReadOnly();

    public void UpdatePassword(string password)
    {
        password.MustNotBeNullOrWhiteSpace();

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

    public void UpdateAvatar(Image avatar)
    {
        AvatarId = avatar.Id;
    }

    public void AssignRole(Role role)
    {
        if (_userRoles.Any(x => x.RoleCode == role.Id))
        {
            return;
        }

        _userRoles.Add(new UserRole(Id, role.Id));
    }

    public void UnassignRole(RoleCode roleCode)
    {
        var userRole = _userRoles.FirstOrDefault(x => x.RoleCode == roleCode);
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
        Salt = RandomNumberGenerator.GetHexString(SaltLength);
    }
}
