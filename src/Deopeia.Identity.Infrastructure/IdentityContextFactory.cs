using Microsoft.EntityFrameworkCore.Design;

namespace Deopeia.Identity.Infrastructure;

internal class IdentityContextFactory : IDesignTimeDbContextFactory<IdentityContext>
{
    public IdentityContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<IdentityContext>();
        optionsBuilder.UseSqlServer(
            "Server=localhost;Database=Identity;User Id=sa;Password=Pass@word;MultipleActiveResultSets=True;Encrypt=False;"
        );

        return new IdentityContext(optionsBuilder.Options);
    }
}
