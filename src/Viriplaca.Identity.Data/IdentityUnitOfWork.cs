namespace Viriplaca.Identity.Data;

public class IdentityUnitOfWork(IdentityContext context)
    : UnitOfWork<IdentityContext>(context), IIdentityUnitOfWork
{
}
