namespace Deopeia.Funds.Domain.Accounts;

public readonly record struct AccountId(Guid Guid) : IEntityId
{
    public AccountId()
        : this(Guid.CreateVersion7()) { }
}
