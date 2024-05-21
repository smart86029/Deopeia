using System.Text.Json;

namespace Viriplaca.Common.JsonConverters;

public class CultureInfoJsonConverter : JsonConverter<CultureInfo>
{
    public override CultureInfo Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        return CultureInfo.GetCultureInfo(reader.GetString()!);
    }

    public override void Write(
        Utf8JsonWriter writer,
        CultureInfo culture,
        JsonSerializerOptions options
    )
    {
        writer.WriteStringValue(culture.ToString());
    }
}
