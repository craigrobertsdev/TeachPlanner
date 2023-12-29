namespace TeachPlanner.Shared.Domain.Common;

public record Location {
    public Location(string streetNumber, string streetName, string suburb) {
        StreetNumber = streetNumber;
        StreetName = streetName;
        Suburb = suburb;
    }

    public string StreetNumber { get; set; }
    public string StreetName { get; set; }
    public string Suburb { get; set; }
}