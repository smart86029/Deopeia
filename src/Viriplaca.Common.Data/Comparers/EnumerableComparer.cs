using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Viriplaca.Common.Data.Comparers;

public class EnumerableComparer<TValue>()
    : ValueComparer<IEnumerable<TValue>>(
        (c1, c2) => c1!.SequenceEqual(c2!),
        c => c.Aggregate(0, (hashCode, value) => HashCode.Combine(hashCode, value.GetHashCode())),
        c => c.ToList())
    where TValue : notnull
{
}
