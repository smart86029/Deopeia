using System.Numerics;

namespace Deopeia.Common.Domain.Finance;

public readonly record struct Amount(decimal Value)
{
    public static Amount operator +(Amount amount)
    {
        return amount;
    }

    public static Amount operator -(Amount amount)
    {
        return new Amount(-amount.Value);
    }

    public static Amount operator +(Amount augend, Amount addend)
    {
        return new Amount(augend.Value + addend.Value);
    }

    public static Amount operator -(Amount minuend, Amount subtrahend)
    {
        return new Amount(minuend.Value - subtrahend.Value);
    }

    public static bool operator <(Amount x, Amount y)
    {
        return x.Value < y.Value;
    }

    public static bool operator >(Amount x, Amount y)
    {
        return x.Value > y.Value;
    }
}
