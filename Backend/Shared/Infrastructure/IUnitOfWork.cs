namespace Shared.Infrastructure;

public interface IUnitOfWork 
{
    Task<bool> CommitAsync();
}
