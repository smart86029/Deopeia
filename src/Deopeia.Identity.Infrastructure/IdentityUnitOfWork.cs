namespace Deopeia.Identity.Infrastructure;

public class IdentityUnitOfWork(IdentityContext context, CurrentUser currentUser)
    : UnitOfWork<IdentityContext>(context, currentUser),
        IIdentityUnitOfWork { }
