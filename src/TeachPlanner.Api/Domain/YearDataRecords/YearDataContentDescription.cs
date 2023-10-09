namespace TeachPlanner.Api.Domain.YearDataRecords;

public record YearDataContentDescription
{
    public string CurriculumCode { get; private set; }
    private readonly List<int> _termsTaughtIn = new();
    public IReadOnlyList<int> TermsTaughtIn => _termsTaughtIn.AsReadOnly();
    public bool Scheduled => _termsTaughtIn.Any();

    private YearDataContentDescription(string curriculumCode)
    {
        CurriculumCode = curriculumCode;
    }

    public static YearDataContentDescription Create(string curriculumCode)
    {
        return new YearDataContentDescription(curriculumCode);
    }
}
