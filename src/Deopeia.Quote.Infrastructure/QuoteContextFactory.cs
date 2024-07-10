using Microsoft.EntityFrameworkCore.Design;

namespace Deopeia.Quote.Infrastructure;

internal class QuoteContextFactory : IDesignTimeDbContextFactory<QuoteContext>
{
    public QuoteContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<QuoteContext>();
        optionsBuilder.UseNpgsql(
            "Server=localhost;Port=5432;User Id=root;Password=Pass@word;Database=Quote;"
        );

        return new QuoteContext(optionsBuilder.Options);
    }
}
