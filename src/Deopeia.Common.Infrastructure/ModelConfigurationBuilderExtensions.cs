namespace Deopeia.Common.Infrastructure;

public static class ModelConfigurationBuilderExtensions
{
    public static ModelConfigurationBuilder ApplyConventions(this ModelConfigurationBuilder builder)
    {
        builder.Properties<CultureInfo>().HaveConversion<CultureInfoConverter>();

        builder.Properties<DateTimeOffset>().HaveConversion<DateTimeOffsetConverter>();

        builder.Properties<Type>().HaveConversion<TypeConverter>();

        return builder;
    }
}
