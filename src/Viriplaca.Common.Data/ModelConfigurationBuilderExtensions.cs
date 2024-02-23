namespace Viriplaca.Common.Data;

public static class ModelConfigurationBuilderExtensions
{
    public static void ApplyConventions(this ModelConfigurationBuilder builder)
    {
        builder
            .Properties<DateTimeOffset>()
            .HaveConversion<DateTimeOffsetConverter>()
            .HaveColumnType("datetime2");
    }
}
