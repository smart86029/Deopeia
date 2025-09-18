namespace Deopeia.Product.Infrastructure;

public sealed class ProductUnitOfWork(ProductContext context)
    : UnitOfWork<ProductContext>(context) { }
