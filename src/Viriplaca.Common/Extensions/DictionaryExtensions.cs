namespace Viriplaca.Common.Extensions;

public static class DictionaryExtensions
{
    public static void AddRange<TKey, TValue>(
        this IDictionary<TKey, TValue> value,
        IDictionary<TKey, TValue> keyValuePairs
    )
    {
        foreach (var pair in keyValuePairs)
        {
            value.Add(pair.Key, pair.Value);
        }
    }
}
