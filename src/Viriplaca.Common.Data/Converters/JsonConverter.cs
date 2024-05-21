using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Viriplaca.Common.JsonConverters;

namespace Viriplaca.Common.Data.Converters;

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
