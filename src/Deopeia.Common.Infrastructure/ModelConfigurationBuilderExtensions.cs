using Deopeia.Common.Domain.Auditing;
using Deopeia.Common.Domain.Files;
using Deopeia.Common.Domain.Finance;
using Deopeia.Common.Domain.Measurement;
using Deopeia.Common.Infrastructure.Auditing;
using Deopeia.Common.Infrastructure.Files;
using Deopeia.Common.Infrastructure.Finance;
using Deopeia.Common.Infrastructure.Measurement;

namespace Deopeia.Common.Infrastructure;

public static class ModelConfigurationBuilderExtensions
{
    public static ModelConfigurationBuilder ApplyConventions(this ModelConfigurationBuilder builder)
    {
        builder.Properties<AuditTrailId>().HaveConversion<AuditTrailIdConverter>();

        builder.Properties<CultureInfo>().HaveConversion<CultureInfoConverter>();

        builder.Properties<CurrencyCode>().HaveConversion<CurrencyCodeConverter>();

        builder.Properties<DateTimeOffset>().HaveConversion<DateTimeOffsetConverter>();

        builder.Properties<ExchangeId>().HaveConversion<ExchangeIdConverter>();

        builder.Properties<FileResourceId>().HaveConversion<FileResourceIdConverter>();

        builder.Properties<Symbol>().HaveConversion<SymbolConverter>();

        builder.Properties<UnitCode>().HaveConversion<UnitCodeConverter>();

        builder.Properties<Type>().HaveConversion<TypeConverter>();

        builder.Properties<TimeZoneInfo>().HaveConversion<TimeZoneInfoConverter>();

        return builder;
    }
}
