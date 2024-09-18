using Deopeia.Quote.Domain.Companies;
using Deopeia.Quote.Infrastructure.Companies;

namespace Deopeia.Quote.Infrastructure;

public class QuoteContext(DbContextOptions<QuoteContext> options) : DbContext(options)
{
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.ApplyConventions();

        configurationBuilder.Properties<CompanyId>().HaveConversion<CompanyIdConverter>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(GetType().Assembly)
            .ApplyCommonConfigurations();
    }
}
