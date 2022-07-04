namespace Shared.Infrastructure;

public interface IUnitOfWork
{
    Task<bool> CommitAsync(bool requireChangesToSuccess = true);
}
