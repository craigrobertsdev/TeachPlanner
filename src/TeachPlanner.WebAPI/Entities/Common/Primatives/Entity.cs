﻿using TeachPlanner.Api.Entities.Common.Interfaces;

namespace TeachPlanner.Api.Entities.Common.Primatives;

public class Entity<TId> : IEquatable<Entity<TId>>, IHasDomainEvents
    where TId : notnull
{
    public List<DomainEvent> _domainEvents = new();
    public TId Id { get; protected set; }
    public IReadOnlyList<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();


    protected Entity(TId id)
    {
        Id = id;
    }

    public override bool Equals(object? obj)
    {
        return obj is Entity<TId> entity && Id.Equals(entity.Id);
    }

    public bool Equals(Entity<TId>? other)
    {
        return Equals((object?)other);
    }

    public static bool operator ==(Entity<TId>? left, Entity<TId>? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Entity<TId>? left, Entity<TId>? right)
    {
        return !Equals(left, right);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public void AddDomainEvent(DomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    protected Entity() { }
}
