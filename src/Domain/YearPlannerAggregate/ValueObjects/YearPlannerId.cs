﻿using Domain.Common.Primatives;

namespace Domain.YearPlannerAggregate.ValueObjects;

public class YearPlannerId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private YearPlannerId(Guid value)
    {
        Value = value;
    }

    public static YearPlannerId Create()
    {
        return new(Guid.NewGuid());
    }

    public static YearPlannerId Create(Guid value)
    {
        return new(value);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private YearPlannerId() { }
}
