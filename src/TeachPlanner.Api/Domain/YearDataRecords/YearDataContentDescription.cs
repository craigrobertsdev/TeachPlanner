namespace TeachPlanner.Api.Domain.YearDataRecords;

public record YearDataContentDescription {
    private readonly List<int> _termsTaughtIn = new();

    private YearDataContentDescription(string curriculumCode) {
        CurriculumCode = curriculumCode;
    }

    public string CurriculumCode { get; private set; }
    public IReadOnlyList<int> TermsTaughtIn => _termsTaughtIn.AsReadOnly();
    public bool Scheduled => _termsTaughtIn.Any();

    public static YearDataContentDescription Create(string curriculumCode) {
        return new YearDataContentDescription(curriculumCode);
    }
}