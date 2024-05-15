using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Viriplaca.Common.Data.Converters;

public class JsonConverter<TValue>()
    : ValueConverter<TValue, string>(value => value!.ToJson(), json => json.ToObject<TValue>()!) { }
