using Microsoft.EntityFrameworkCore.Design;

namespace Viriplaca.Identity.Data;

internal class IdentityContextFactory : IDesignTimeDbContextFactory<IdentityContext>
{
    public IdentityContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<IdentityContext>();
        optionsBuilder.UseSqlServer("Server=localhost;Database=ViriplacaIdentity;User Id=sa;Password=Pass@word;MultipleActiveResultSets=True;Encrypt=False;");

        return new IdentityContext(optionsBuilder.Options);
    }
}
