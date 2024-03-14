namespace Viriplaca.Identity.Domain.Users;

public class UserRefreshToken : Entity
{
    private UserRefreshToken()
    {
    }

    internal UserRefreshToken(string refreshToken, DateTimeOffset expiredAt, Guid userId)
    {
        expiredAt.MustBeAfterNow();

        UserId = userId;
        RefreshToken = refreshToken;
        ExpiredAt = expiredAt;
    }

    public Guid UserId { get; private init; }

    public string RefreshToken { get; private init; } = string.Empty;

    public DateTimeOffset IssuedAt { get; private init; } = DateTimeOffset.UtcNow;

    public DateTimeOffset ExpiredAt { get; private init; }

    public DateTimeOffset? RevokedAt { get; private set; }

    public bool IsExpired => DateTimeOffset.UtcNow > ExpiredAt;

    public bool IsRevoked => RevokedAt is not null;

    public bool IsActive => !IsExpired && !IsRevoked;

    public void Revoke()
    {
        RevokedAt = DateTimeOffset.UtcNow;
    }
}
