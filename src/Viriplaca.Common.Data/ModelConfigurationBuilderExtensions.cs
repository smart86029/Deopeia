namespace Viriplaca.Common.Data;

public static class ModelConfigurationBuilderExtensions
{
    public static ModelConfigurationBuilder ApplyConventions(this ModelConfigurationBuilder builder)
    {
        builder
            .Properties<CultureInfo>()
            .HaveConversion<CultureInfoConverter>()
            .HaveMaxLength(16);

        builder
            .Properties<DateTimeOffset>()
            .HaveConversion<DateTimeOffsetConverter>()
            .HaveColumnType("datetime2");

        return builder;
    }
}
