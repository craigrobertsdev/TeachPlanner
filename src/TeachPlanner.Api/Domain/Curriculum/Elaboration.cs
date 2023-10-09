namespace TeachPlanner.Api.Domain.CurriculumSubjects;

public record Elaboration
{
    public string Description { get; private set; }

    private Elaboration(string description)
    {
        Description = description;
    }

    public static Elaboration Create(string description)
    {
        return new(description);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Elaboration() { }
}
