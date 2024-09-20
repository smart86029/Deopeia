using Deopeia.Common.Infrastructure.Auditing;
using Deopeia.Common.Infrastructure.Files;
using Deopeia.Common.Infrastructure.Finance;
using Deopeia.Common.Infrastructure.Localization;

namespace Deopeia.Common.Infrastructure;

public static class ModelBuilderExtensions
{
    public static ModelBuilder ApplyCommonConfigurations(this ModelBuilder builder)
    {
        builder
            .ApplyConfiguration(new AuditTrailConfiguration())
            .ApplyConfiguration(new CurrencyConfiguration())
            .ApplyConfiguration(new CurrencyLocaleConfiguration())
            .ApplyConfiguration(new DataAccessAuditTrailConfiguration())
            .ApplyConfiguration(new FileResourceConfiguration())
            .ApplyConfiguration(new ImageConfiguration())
            .ApplyConfiguration(new LocaleResourceConfiguration())
            .Ignore<DomainEvent>();

        return builder;
    }
}
