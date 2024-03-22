using Microsoft.EntityFrameworkCore.Design;

namespace Viriplaca.HR.Data;

internal class HRContextFactory : IDesignTimeDbContextFactory<HRContext>
{
    public HRContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<HRContext>();
        optionsBuilder.UseSqlServer("Server=localhost;Database=ViriplacaHR;User Id=sa;Password=Pass@word;MultipleActiveResultSets=True;Encrypt=False");

        return new HRContext(optionsBuilder.Options);
    }
}
