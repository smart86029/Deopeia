using Deopeia.Product.Domain.Instruments;
using Deopeia.Product.Infrastructure.Instruments;

namespace Deopeia.Product.Infrastructure;

public sealed class ProductContext(DbContextOptions<ProductContext> options) : DbContext(options)
{
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.ApplyConventions();

        configurationBuilder.Properties<InstrumentId>().HaveConversion<InstrumentIdConverter>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(GetType().Assembly)
            .ApplyCommonConfigurations();
    }
}
