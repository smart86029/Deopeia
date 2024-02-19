namespace Viriplaca.Identity.Data;

public class IdentityUnitOfWork(IdentityContext context)
    : IIdentityUnitOfWork
{
    private readonly IdentityContext _context = context;

    public async Task<bool> CommitAsync()
    {
        await _context.SaveChangesAsync();

        return true;
    }
}
