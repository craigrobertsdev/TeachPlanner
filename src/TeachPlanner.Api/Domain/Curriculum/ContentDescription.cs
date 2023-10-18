namespace TeachPlanner.Api.Domain.CurriculumSubjects;

public record ContentDescription
{
    private readonly List<Elaboration> _elaborations = new();

    private ContentDescription(string description, string curriculumCode, List<Elaboration> elaborations,
        string substrand)
    {
        Description = description;
        CurriculumCode = curriculumCode;
        _elaborations = elaborations;
        Substrand = substrand;
    }

    public string Description { get; private set; }
    public string CurriculumCode { get; private set; }
    public string Substrand { get; private set; } = string.Empty;
    public IReadOnlyList<Elaboration> Elaborations => _elaborations.AsReadOnly();

    public static ContentDescription Create(string description, string curriculumCode, List<Elaboration> elaborations,
        string substrand = "")
    {
        return new ContentDescription(description, curriculumCode, elaborations, substrand);
    }

    public void AddElaboration(Elaboration elaboration)
    {
        _elaborations.Add(elaboration);
    }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private ContentDescription()
    {
    }
}