using Deopeia.Common.Domain.Files;
using Deopeia.Common.Domain.Finance;
using Deopeia.Common.Infrastructure.Files;
using Deopeia.Common.Infrastructure.Finance;

namespace Deopeia.Common.Infrastructure;

public static class ModelConfigurationBuilderExtensions
{
    public static ModelConfigurationBuilder ApplyConventions(this ModelConfigurationBuilder builder)
    {
        builder.Properties<CultureInfo>().HaveConversion<CultureInfoConverter>();
        builder.Properties<DateTimeOffset>().HaveConversion<DateTimeOffsetConverter>();
        builder.Properties<Type>().HaveConversion<TypeConverter>();
        builder.Properties<TimeZoneInfo>().HaveConversion<TimeZoneInfoConverter>();

        builder.Properties<FileResourceId>().HaveConversion<FileResourceIdConverter>();
        builder.Properties<Symbol>().HaveConversion<SymbolConverter>();

        return builder;
    }
}
