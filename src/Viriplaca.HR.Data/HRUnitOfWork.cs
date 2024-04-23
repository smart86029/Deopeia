namespace Viriplaca.HR.Data;

public class HRUnitOfWork(HRContext context)
    : UnitOfWork<HRContext>(context), IHRUnitOfWork
{
}
