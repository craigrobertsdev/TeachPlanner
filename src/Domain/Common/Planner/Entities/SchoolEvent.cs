using Domain.Common.Planner.ValueObjects;
using Domain.Common.Primatives;

namespace Domain.Common.Planner.Entities;

public class SchoolEvent : Entity<SchoolEventId>
{
    public Location Location { get; private set; }
    public string Name { get; private set; }
    public DateTime EventStart { get; private set; }
    public DateTime EventEnd { get; private set; }
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    private SchoolEvent(
        SchoolEventId id,
        Location location,
        string name,
        DateTime eventStart,
        DateTime eventEnd) : base(id)
    {
        Location = location;
        Name = name;
        EventStart = eventStart;
        EventEnd = eventEnd;
        CreatedDateTime = DateTime.UtcNow;
        UpdatedDateTime = DateTime.UtcNow;
    }

    public static SchoolEvent Create(
        Location location,
        string name,
        DateTime eventStart,
        DateTime eventEnd)
    {
        return new SchoolEvent(
            new SchoolEventId(Guid.NewGuid()),
            location,
            name,
            eventStart,
            eventEnd);
    }
}
