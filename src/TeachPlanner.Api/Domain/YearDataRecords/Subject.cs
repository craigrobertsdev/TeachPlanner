namespace TeachPlanner.Api.Domain.YearDataRecords;

public record Subject
{
    private readonly List<YearDataContentDescription> _contentDescriptions = new();
    public string Name { get; private set; }
    public IReadOnlyList<YearDataContentDescription> ContentDescriptions => _contentDescriptions.AsReadOnly();

    private Subject(string name, List<YearDataContentDescription> contentDescriptions)
    {
        Name = name;
        _contentDescriptions = contentDescriptions;
    }

    public static Subject Create(string name, List<YearDataContentDescription> contentDescriptions)
    {
        return new Subject(name, contentDescriptions);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Subject() { }

    /*
     * A teacher will have a list of subjects that are just references to the curriculum subjects
     * When a CD is added to a lesson plan or term plan, it will be added to the subject
     * When the subjects are requested, they will be queried from the curriculum subjects table
     * based on the subject name, year levels and content description ids in the _contentDescriptions list
     * 
     * 
     */
}
