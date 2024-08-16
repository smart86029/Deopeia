using Deopeia.Quote.Domain.Companies;
using Deopeia.Quote.Domain.Instruments;
using Deopeia.Quote.Infrastructure.Companies;
using Deopeia.Quote.Infrastructure.Instruments;

namespace Deopeia.Quote.Infrastructure;

public class QuoteContext(DbContextOptions<QuoteContext> options) : DbContext(options)
{
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.ApplyConventions();

        configurationBuilder.Properties<CompanyId>().HaveConversion<CompanyIdConverter>();

        configurationBuilder.Properties<InstrumentId>().HaveConversion<InstrumentIdConverter>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(GetType().Assembly)
            .ApplyCommonConfigurations();
    }
}
