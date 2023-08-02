namespace Domain.Common.Primatives;

public abstract class Entity : IEquatable<Entity>
{
    public virtual Guid Id { get; protected set; }

    protected Entity(Guid id)
    {
        Id = id;
    }

    public override bool Equals(object? obj)
    {
        return obj is Entity entity && Id.Equals(entity.Id);
    }

    public bool Equals(Entity? other)
    {
        return Equals((object?)other);
    }

    public static bool operator ==(Entity? left, Entity? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Entity? left, Entity? right)
    {
        return !Equals(left, right);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    //public void AddDomainEvent(IDomainEvent domainEvent)
    //{
    //    _domainEvents.Add(domainEvent);
    //}

    //public void ClearDomainEvents()
    //{
    //    _domainEvents.Clear();
    //}

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    protected Entity()
    {
    }
#pragma warning restore CS8618

}
