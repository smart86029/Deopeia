namespace Deopeia.Identity.Domain.Users;

public readonly record struct UserRefreshTokenId(Guid Guid) : IEntityId
{
    public UserRefreshTokenId()
        : this(GuidUtility.NewGuid()) { }
}
