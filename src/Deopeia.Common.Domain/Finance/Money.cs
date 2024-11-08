namespace Deopeia.Common.Domain.Finance;

public readonly record struct Money(CurrencyCode CurrencyCode, decimal Amount)
{
    public static Money operator +(Money augend, Money addend)
    {
        if (augend.CurrencyCode != addend.CurrencyCode)
        {
            throw new ArgumentException();
        }

        return new Money(augend.CurrencyCode, augend.Amount + addend.Amount);
    }

    public static Money operator -(Money minuend, Money subtrahend)
    {
        if (minuend.CurrencyCode != subtrahend.CurrencyCode)
        {
            throw new ArgumentException();
        }

        return new Money(minuend.CurrencyCode, minuend.Amount - subtrahend.Amount);
    }
}
