using Domain.Common.Primatives;

namespace Domain.SubjectAggregates;

public sealed class ContentDescription : ValueObject
{
    private readonly List<Elaboration> _elaborations = new();
    public string Description { get; private set; }
    public string CurriculumCode { get; private set; }
    public IReadOnlyList<Elaboration> Elaborations => _elaborations.AsReadOnly();
    public Substrand? Substrand { get; private set; }
    public Strand? Strand { get; private set; }

    private ContentDescription(string description, string curriculumCode, List<Elaboration> elaborations, Strand? strand, Substrand? substrand)
    {
        Description = description;
        CurriculumCode = curriculumCode;
        _elaborations = elaborations;
        Strand = strand;
        Substrand = substrand;
    }

    public static ContentDescription Create(string description, string curriculumCode, List<Elaboration> elaborations, Strand? strand = null, Substrand? substrand = null)
    {
        return new(description, curriculumCode, elaborations, strand, substrand);
    }

    public void AddElaboration(Elaboration elaboration)
    {
        _elaborations.Add(elaboration);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Description;
        yield return CurriculumCode;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private ContentDescription()
    {
    }
}
