namespace Viriplaca.Identity.Data;

public class HRUnitOfWork(HRContext context)
    : IHRUnitOfWork
{
    private readonly HRContext _context = context;

    public async Task<bool> CommitAsync()
    {
        await _context.SaveChangesAsync();

        return true;
    }
}
