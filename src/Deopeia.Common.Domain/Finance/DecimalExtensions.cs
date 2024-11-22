namespace Deopeia.Common.Domain.Finance;

public static class DecimalExtensions
{
    public static Money ToMoney(this decimal amount, CurrencyCode currencyCode)
    {
        return new Money(currencyCode, amount);
    }
}
