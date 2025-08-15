namespace Deopeia.Common.Infrastructure;

public abstract class DbSeeder
{
    public abstract void Seed(DbContext context);
}
