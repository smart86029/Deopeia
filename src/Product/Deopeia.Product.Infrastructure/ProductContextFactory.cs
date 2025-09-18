using Microsoft.EntityFrameworkCore.Design;

namespace Deopeia.Product.Infrastructure;

internal sealed class ProductContextFactory : IDesignTimeDbContextFactory<ProductContext>
{
    public ProductContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ProductContext>();
        optionsBuilder.UseNpgsql().UseSnakeCaseNamingConvention();
        return new ProductContext(optionsBuilder.Options);
    }
}
