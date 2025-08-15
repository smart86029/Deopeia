using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Deopeia.Common.Infrastructure.Comparers;

public class DictionaryComparer<TKey, TValue>()
    : ValueComparer<IReadOnlyDictionary<TKey, TValue>>(
        (c1, c2) => c1!.SequenceEqual(c2!),
        c => c.Aggregate(0, (hashCode, pair) => HashCode.Combine(hashCode, pair.GetHashCode())),
        c => c.ToDictionary()
    )
    where TKey : notnull { }
