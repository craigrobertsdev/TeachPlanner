namespace TeachPlanner.Api.Database;

public class DbContextSettings {
    public const string SectionName = "ConnectionStrings";
    public string DefaultConnection { get; init; } = null!;
}