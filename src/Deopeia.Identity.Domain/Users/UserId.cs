namespace Deopeia.Identity.Domain.Users;

public readonly record struct UserId(Guid Guid) : IEntityId
{
    public UserId()
        : this(GuidUtility.NewGuid()) { }
}
