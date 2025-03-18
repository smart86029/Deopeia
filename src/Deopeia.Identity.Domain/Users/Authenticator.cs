using System.Security.Cryptography;

namespace Deopeia.Identity.Domain.Users;

public class Authenticator : Entity<UserId>
{
    private const string Base32Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";

    /// <summary>
    /// RFC 4226 (https://datatracker.ietf.org/doc/html/rfc4226#section-4)
    /// </summary>
    private const int SecretKeyLength = 32;

    private Authenticator() { }

    public Authenticator(UserId userId)
        : base(userId) { }

    public BindingStatus BindingStatus { get; private set; }

    public string? SecretKey { get; private set; }

    public int ErrorCount { get; private set; }

    public DateTimeOffset? LockedAt { get; private set; }

    public bool IsLocked => LockedAt.HasValue;

    public void Bind()
    {
        BindingStatus = BindingStatus.Bound;
        ResetErrorCount();
    }

    public void IncrementErrorCount()
    {
        if (IsLocked)
        {
            return;
        }

        ErrorCount++;
        if (ErrorCount >= 5)
        {
            LockedAt = DateTimeOffset.UtcNow;
        }
    }

    public void ResetErrorCount()
    {
        ErrorCount = 0;
        LockedAt = null;
    }

    public void CreateSecretKey()
    {
        if (BindingStatus == BindingStatus.Bound)
        {
            throw new DomainException("Authenticator.AlreadyBound");
        }

        SecretKey = RandomNumberGenerator.GetString(Base32Alphabet, SecretKeyLength);
    }
}
