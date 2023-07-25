namespace Domain.Common.Planner;

public class Location
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
}
