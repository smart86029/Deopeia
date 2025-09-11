namespace Deopeia.Common.Infrastructure;

public static class ModelBuilderExtensions
{
    public static ModelBuilder ApplyCommonConfigurations(this ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(ModelBuilderExtensions).Assembly);
        builder.Ignore<DomainEvent>();

        return builder;
    }
}
