namespace TeachPlanner.Api.Domain.CurriculumSubjects;

public record Elaboration
{
    private Elaboration(string description)
    {
        Description = description;
    }

    public string Description { get; private set; }

    public static Elaboration Create(string description)
    {
        return new Elaboration(description);
    }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Elaboration()
    {
    }
}