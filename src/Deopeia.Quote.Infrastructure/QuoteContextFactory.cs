using Microsoft.EntityFrameworkCore.Design;

namespace Deopeia.Quote.Infrastructure;

internal class QuoteContextFactory : IDesignTimeDbContextFactory<QuoteContext>
{
    public QuoteContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<QuoteContext>();
        optionsBuilder.UseNpgsql().UseSnakeCaseNamingConvention();

        return new QuoteContext(optionsBuilder.Options);
    }
}
