namespace Domain.Common.Primatives;

public abstract class AggregateRoot<TId> : Entity<TId> where TId : notnull
{
    protected AggregateRoot(TId id)
    {
        Id = id;
    }

    protected AggregateRoot()
    {
    }
}
