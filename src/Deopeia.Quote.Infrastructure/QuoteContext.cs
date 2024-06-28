namespace Deopeia.Quote.Infrastructure;

public class QuoteContext(DbContextOptions<QuoteContext> options) : DbContext(options)
{
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.ApplyConventions();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("Quote")
            .ApplyConfigurationsFromAssembly(GetType().Assembly)
            .ApplyCommonConfigurations();
    }
}
