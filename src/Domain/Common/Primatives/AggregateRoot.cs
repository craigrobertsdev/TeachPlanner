namespace Domain.Common.Primatives;

//public abstract partial class AggregateRoot<TId, TIdType> : Entity<TId>
//    where TId : AggregateRootId<TIdType>
//{
//    public new AggregateRootId<TIdType> Id { get; protected set; }

//    protected AggregateRoot(TId id)
//    {
//        Id = id;
//    }

//#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
//    protected AggregateRoot()
//    {
//    }
//}

public abstract partial class AggregateRoot<TId> : Entity<TId>
    where TId : notnull
{
    public AggregateRoot(TId id) : base(id)
    {

    }

    protected AggregateRoot()
    {

    }
}
