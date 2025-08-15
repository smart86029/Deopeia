namespace Deopeia.Common.Application;

public interface IUnitOfWork
{
    Task CommitAsync(CancellationToken cancellationToken = default);
}
