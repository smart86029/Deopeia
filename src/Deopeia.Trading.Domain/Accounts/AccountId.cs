namespace Deopeia.Trading.Domain.Accounts;

public readonly record struct AccountId(Guid Guid) : IEntityId
{
    public AccountId()
        : this(GuidUtility.NewGuid()) { }
}
