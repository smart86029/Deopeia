using System.Text.Json;
using Deopeia.Common.JsonConverters;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Deopeia.Common.Infrastructure.Converters;

public class JsonConverter<TValue>()
    : ValueConverter<TValue, string>(
        value => value!.ToJson(Options),
        json => json.ToObject<TValue>()!
    )
{
    private static readonly JsonSerializerOptions Options = new();

    static JsonConverter()
    {
        Options.Converters.Add(new CultureInfoJsonConverter());
    }
}
