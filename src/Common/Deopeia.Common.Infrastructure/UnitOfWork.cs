using Deopeia.Common.Application;

namespace Deopeia.Common.Infrastructure;

public abstract class UnitOfWork<TContext>(TContext context) : IUnitOfWork
    where TContext : DbContext
{
    private readonly TContext _context = context;

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
