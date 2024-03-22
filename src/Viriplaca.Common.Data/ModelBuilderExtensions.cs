using Viriplaca.Common.Data.Configurations;

namespace Viriplaca.Common.Data;

public static class ModelBuilderExtensions
{
    public static ModelBuilder ApplyCommonConfigurations(this ModelBuilder builder)
    {
        builder
            .ApplyConfiguration(new LocaleResourceConfiguration())
            .ApplyConfiguration(new FileResourceConfiguration())
            .ApplyConfiguration(new ImageConfiguration())
            .Ignore<DomainEvent>();

        return builder;
    }
}
