namespace Shared.Infrastructure;

public abstract class Entity
{
    public Guid Id { get; private set; }

    public Entity(Guid id)
    {
        Id = id;
    }
}
