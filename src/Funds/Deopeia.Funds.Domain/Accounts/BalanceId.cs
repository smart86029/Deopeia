namespace Deopeia.Funds.Domain.Accounts;

public readonly record struct BalanceId(AccountId AccountId, AssetCode AssetCode) : IEntityId;
