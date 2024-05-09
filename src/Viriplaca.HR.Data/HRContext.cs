using Viriplaca.Common.Data.Converters;

namespace Viriplaca.HR.Data;

public class HRContext(DbContextOptions<HRContext> options)
    : DbContext(options)
{
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.ApplyConventions();
        configurationBuilder
            .Properties<WorkingTime>()
            .HaveConversion<WorkingTimeConverter>()
            .HaveColumnType("decimal(18,2)");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("HR")
            .ApplyConfigurationsFromAssembly(GetType().Assembly)
            .ApplyCommonConfigurations();
    }
}
