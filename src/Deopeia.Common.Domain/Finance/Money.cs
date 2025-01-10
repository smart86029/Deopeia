namespace Deopeia.Common.Domain.Finance;

public readonly record struct Money(CurrencyCode CurrencyCode, decimal Amount)
{
    public Money()
        : this(CurrencyCode.Default, 0) { }

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

    public static bool operator <(Money x, Money y)
    {
        return x.Amount < y.Amount;
    }

    public static bool operator >(Money x, Money y)
    {
        return x.Amount > y.Amount;
    }
}
