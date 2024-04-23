using Viriplaca.Common.Data.Auditing;
using Viriplaca.Common.Data.Files;
using Viriplaca.Common.Data.Localization;

namespace Viriplaca.Common.Data;

public static class ModelBuilderExtensions
{
    public static ModelBuilder ApplyCommonConfigurations(this ModelBuilder builder)
    {
        builder
            .ApplyConfiguration(new AuditTrailConfiguration())
            .ApplyConfiguration(new DataAccessAuditTrailConfiguration())
            .ApplyConfiguration(new FileResourceConfiguration())
            .ApplyConfiguration(new ImageConfiguration())
            .ApplyConfiguration(new LocaleResourceConfiguration())
            .Ignore<DomainEvent>();

        return builder;
    }
}
