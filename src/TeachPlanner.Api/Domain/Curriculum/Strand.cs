namespace TeachPlanner.Api.Domain.CurriculumSubjects;

public record Strand
{
    public string Name { get; private set; }
    private readonly List<ContentDescription> _contentDescriptions = new();
    public IReadOnlyList<ContentDescription> ContentDescriptions => _contentDescriptions.AsReadOnly();

    private Strand(
        string name,
        List<ContentDescription> contentDescriptions
    )
    {
        Name = name;
        _contentDescriptions = contentDescriptions;
    }

    public static Strand Create(
        string name,
        List<ContentDescription> contentDescriptions
    )
    {
        return new Strand(name, contentDescriptions);
    }

    public void AddContentDescriptions(List<ContentDescription> contentDescriptions)
    {
        foreach (var contentDescription in contentDescriptions)
        {
            AddContentDescription(contentDescription);
        }
    }

    public void AddContentDescription(ContentDescription contentDescription)
    {
        if (!_contentDescriptions!.Contains(contentDescription))
        {
            _contentDescriptions!.Add(contentDescription);
        }
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Strand() { }
}
