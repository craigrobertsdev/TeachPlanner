namespace Domain.Models;

public class Subject
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<YearLevel> YearLevels { get; set; } = new();
}
