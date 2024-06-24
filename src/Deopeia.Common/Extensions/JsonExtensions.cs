using System.Text.Json;

namespace Deopeia.Common.Extensions;

public static class JsonExtensions
{
    public static string ToJson(this object value)
    {
        return JsonSerializer.Serialize(value);
    }

    public static string ToJson(this object value, JsonSerializerOptions options)
    {
        return JsonSerializer.Serialize(value, options);
    }

    public static byte[] ToUtf8Bytes(this object value)
    {
        return JsonSerializer.SerializeToUtf8Bytes(value);
    }

    public static T? ToObject<T>(this string value)
    {
        return JsonSerializer.Deserialize<T>(value);
    }

    public static T? ToObject<T>(this string value, JsonSerializerOptions options)
    {
        return JsonSerializer.Deserialize<T>(value, options);
    }

    public static T? ToObject<T>(this ReadOnlySpan<byte> value)
    {
        return JsonSerializer.Deserialize<T>(value);
    }

    public static object? ToObject(this string value, Type type)
    {
        return JsonSerializer.Deserialize(value, type);
    }

    public static object? ToObject(this ReadOnlySpan<byte> value, Type type)
    {
        return JsonSerializer.Deserialize(value, type);
    }

    public static T DeepClone<T>(this T value)
    {
        return value!.ToJson().ToObject<T>()!;
    }
}
