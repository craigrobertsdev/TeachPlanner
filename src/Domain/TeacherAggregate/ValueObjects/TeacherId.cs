﻿using Domain.Common.Primatives;

namespace Domain.TeacherAggregate.ValueObjects;

public class TeacherId : ValueObject
{
    public Guid Value { get; private set; }

    private TeacherId(Guid value)
    {
        Value = value;
    }

    public static TeacherId Create()
    {
        return new(Guid.NewGuid());
    }
    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
