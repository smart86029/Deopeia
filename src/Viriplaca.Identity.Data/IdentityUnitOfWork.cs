namespace Viriplaca.Identity.Data;

public class IdentityUnitOfWork(IdentityContext context, CurrentUser currentUser)
    : UnitOfWork<IdentityContext>(context, currentUser),
        IIdentityUnitOfWork { }
