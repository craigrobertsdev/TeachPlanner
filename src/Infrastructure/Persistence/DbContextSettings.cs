namespace Infrastructure.Persistence;

public class DbContextSettings
{
    public const string SectionName = "ConnectionStrings";
    public string DefaultConnection { get; init; } = null!;
}
