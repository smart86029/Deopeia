namespace Deopeia.Product.Infrastructure;

public sealed class ProductSeeder : DbSeeder
{
    public override void Seed(DbContext context)
    {
        context.SaveChanges();
    }
}
