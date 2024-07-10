using Microsoft.EntityFrameworkCore.Design;

namespace Deopeia.Identity.Infrastructure;

internal class IdentityContextFactory : IDesignTimeDbContextFactory<IdentityContext>
{
    public IdentityContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<IdentityContext>();
        optionsBuilder.UseNpgsql(
            "Server=localhost;Port=5432;User Id=root;Password=Pass@word;Database=Identity;"
        );

        return new IdentityContext(optionsBuilder.Options);
    }
}
