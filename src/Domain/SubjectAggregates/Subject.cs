using Domain.Common.Primatives;
using Domain.SubjectAggregates.Entities;
using Domain.SubjectAggregates.ValueObjects;

namespace Domain.SubjectAggregates;

public sealed class Subject : AggregateRoot<SubjectId>
{
    private readonly List<YearLevel> _yearLevels = new();
    public string Name { get; private set; }
    public IReadOnlyList<YearLevel> YearLevels => _yearLevels.AsReadOnly();
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    private Subject(
        SubjectId id,
        List<YearLevel> yearLevels,
        string name,
        DateTime createdDateTime,
        DateTime updatedDateTime
    )
        : base(id)
    {
        _yearLevels = yearLevels;
        Name = name;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
    }

    public static Subject Create(
        string name,
        List<YearLevel> yearLevels,
        DateTime createdDateTime,
        DateTime updatedDateTime)
    {
        return new Subject(
            SubjectId.Create(),
            yearLevels,
            name,
            createdDateTime,
            updatedDateTime);

    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Subject() { }
}

