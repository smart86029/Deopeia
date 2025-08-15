using Microsoft.EntityFrameworkCore.Design;

namespace Deopeia.Identity.Infrastructure;

internal class IdentityContextFactory : IDesignTimeDbContextFactory<IdentityContext>
{
    public IdentityContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<IdentityContext>();
        optionsBuilder.UseNpgsql().UseSnakeCaseNamingConvention();
        return new IdentityContext(optionsBuilder.Options);
    }
}
