namespace TeachPlanner.Api.Domain.Subjects;

public record Elaboration
{
    public string Description { get; private set; }
    public ContentDescription ContentDescription { get; private set; }

    private Elaboration(string description, ContentDescription contentDescription)
    {
        Description = description;
        ContentDescription = contentDescription;
    }

    public static Elaboration Create(string description, ContentDescription contentDescription)
    {
        return new(description, contentDescription);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Elaboration() { }
}
