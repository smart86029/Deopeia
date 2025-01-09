namespace Deopeia.Common.Domain.Finance;

public readonly record struct CurrencyCode(string Value) : IEntityId
{
    public static CurrencyCode Default => new("USD");
}
