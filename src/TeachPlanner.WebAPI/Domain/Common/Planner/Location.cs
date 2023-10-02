using TeachPlanner.Api.Domain.Common.Primatives;

namespace TeachPlanner.Api.Domain.Common.Planner;

public class Location : ValueObject
{
    public string StreetNumber { get; set; }
    public string StreetName { get; set; }
    public string Suburb { get; set; }

    public Location(string streetNumber, string streetName, string suburb)
    {
        StreetNumber = streetNumber;
        StreetName = streetName;
        Suburb = suburb;
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return StreetNumber;
        yield return StreetName;
        yield return Suburb;
    }
}
