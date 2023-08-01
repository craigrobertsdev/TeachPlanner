using Domain.Common.Primatives;

namespace Domain.Common.Planner;

public class SchoolEvent : Entity<SchoolEventId>
{
    public Location Location { get; private set; }
    public string Name { get; private set; }
    public bool FullDay { get; private set; }
    public DateTime EventStart { get; private set; }
    public DateTime EventEnd { get; private set; }
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    private SchoolEvent(
        SchoolEventId id,
        Location location,
        string name,
        bool fullDay,
        DateTime eventStart,
        DateTime eventEnd) : base(id)
    {
        Location = location;
        Name = name;
        FullDay = fullDay;
        EventStart = eventStart;
        EventEnd = eventEnd;
        CreatedDateTime = DateTime.UtcNow;
        UpdatedDateTime = DateTime.UtcNow;
    }

    public static SchoolEvent Create(
        Location location,
        string name,
        bool fullDay,
        DateTime eventStart,
        DateTime eventEnd)
    {
        return new SchoolEvent(
            new SchoolEventId(Guid.NewGuid()),
            location,
            name,
            fullDay,
            eventStart,
            eventEnd);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private SchoolEvent() { }
}
