namespace Viriplaca.HR.Data;

public class HRUnitOfWork(HRContext context, CurrentUser currentUser)
    : UnitOfWork<HRContext>(context, currentUser), IHRUnitOfWork
{
}
