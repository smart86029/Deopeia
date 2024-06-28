using Microsoft.EntityFrameworkCore.Design;

namespace Deopeia.Quote.Infrastructure;

internal class QuoteContextFactory : IDesignTimeDbContextFactory<QuoteContext>
{
    public QuoteContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<QuoteContext>();
        optionsBuilder.UseSqlServer(
            "Server=localhost;Database=Quote;User Id=sa;Password=Pass@word;MultipleActiveResultSets=True;Encrypt=False;"
        );

        return new QuoteContext(optionsBuilder.Options);
    }
}
