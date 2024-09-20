using Deopeia.Common.Domain.Auditing;
using Deopeia.Common.Domain.Files;
using Deopeia.Common.Domain.Finance;
using Deopeia.Common.Infrastructure.Auditing;
using Deopeia.Common.Infrastructure.Files;

namespace Deopeia.Common.Infrastructure;

public static class ModelConfigurationBuilderExtensions
{
    public static ModelConfigurationBuilder ApplyConventions(this ModelConfigurationBuilder builder)
    {
        builder.Properties<AssetId>().HaveConversion<AssetIdConverter>();

        builder.Properties<AuditTrailId>().HaveConversion<AuditTrailIdConverter>();

        builder.Properties<CultureInfo>().HaveConversion<CultureInfoConverter>();

        builder.Properties<CurrencyCode>().HaveConversion<CurrencyCodeConverter>();

        builder.Properties<DateTimeOffset>().HaveConversion<DateTimeOffsetConverter>();

        builder.Properties<ExchangeId>().HaveConversion<ExchangeIdConverter>();

        builder.Properties<FileResourceId>().HaveConversion<FileResourceIdConverter>();

        builder.Properties<InstrumentId>().HaveConversion<InstrumentIdConverter>();

        builder.Properties<Type>().HaveConversion<TypeConverter>();

        builder.Properties<TimeZoneInfo>().HaveConversion<TimeZoneInfoConverter>();

        return builder;
    }
}
