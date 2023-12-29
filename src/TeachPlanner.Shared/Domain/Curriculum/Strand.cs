namespace TeachPlanner.Shared.Domain.Curriculum;

public record Strand {
    private readonly List<ContentDescription> _contentDescriptions = new();

    private Strand(
        string name,
        List<ContentDescription> contentDescriptions
    ) {
        Name = name;
        _contentDescriptions = contentDescriptions;
    }

    public string Name { get; private set; }
    public IReadOnlyList<ContentDescription> ContentDescriptions => _contentDescriptions.AsReadOnly();

    public static Strand Create(
        string name,
        List<ContentDescription> contentDescriptions
    ) {
        return new Strand(name, contentDescriptions);
    }

    public void AddContentDescriptions(List<ContentDescription> contentDescriptions) {
        foreach (var contentDescription in contentDescriptions) AddContentDescription(contentDescription);
    }

    public void AddContentDescription(ContentDescription contentDescription) {
        if (!_contentDescriptions!.Contains(contentDescription)) _contentDescriptions!.Add(contentDescription);
    }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Strand() {
    }
}