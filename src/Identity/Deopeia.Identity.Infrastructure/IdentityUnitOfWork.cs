namespace Deopeia.Identity.Infrastructure;

public sealed class IdentityUnitOfWork(IdentityContext context)
    : UnitOfWork<IdentityContext>(context) { }
