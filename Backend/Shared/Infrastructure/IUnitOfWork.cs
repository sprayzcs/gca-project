namespace Shared.Infrastructure;

public interface IUnitOfWork
{
    Task<bool> CommitAsync(bool requireChangesToSuccess = true, CancellationToken cancellationToken = default);

    Task<bool> CommitAsync(CancellationToken cancellationToken) => CommitAsync(true, cancellationToken);
}
