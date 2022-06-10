namespace Shared.Infrastructure;

public abstract class Entity
{
    public Guid Id { get; }

    public Entity(Guid id)
    {
        Id = id;
    }
}
