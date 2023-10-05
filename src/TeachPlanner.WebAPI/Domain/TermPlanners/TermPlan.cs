using TeachPlanner.Api.Common.Exceptions;
using TeachPlanner.Api.Domain.Subjects;

namespace TeachPlanner.Api.Domain.TermPlanners;
public record TermPlan
{
    private readonly List<Subject> _subjects = new();
    public IReadOnlyList<Subject> Subjects => _subjects.AsReadOnly();
    public TermPlanner TermPlanner { get; private set; } = null!;
    public int TermNumber { get; private set; }

    private TermPlan(TermPlanner termPlanner, int termNumber, List<Subject> subjects)
    {
        TermPlanner = termPlanner;
        TermNumber = termNumber;
        _subjects = subjects;
    }

    public void SetSubjects(List<Subject> subjects)
    {
        if (_subjects.Count > 0)
        {
            throw new TermPlanSubjectsAlreadySetException();
        }

        _subjects.AddRange(subjects);
    }

    public void SetSubjectAtIndex(Subject subject, int index)
    {
        _subjects[index] = subject;
    }

    public void AddContentDescription(ContentDescription contentDescription)
    {
        var substrand = contentDescription.Substrand ?? null;
        var strand = substrand?.Strand ?? contentDescription.Strand;
        var yearLevel = strand!.YearLevel;
        var subject = yearLevel.Subject;

        var termPlanSubject = _subjects.FirstOrDefault(s => s.Name == subject.Name);
        if (termPlanSubject is null)
        {
            _subjects.Add(subject);
            return;
        }

        var termPlanYearLevel = termPlanSubject!.YearLevels.FirstOrDefault(yl => yl.Name == yearLevel.Name);
        if (termPlanYearLevel is null)
        {
            termPlanSubject.AddYearLevel(yearLevel);
            return;
        }

        var termPlanStrand = termPlanYearLevel!.Strands.FirstOrDefault(s => s.Name == strand.Name);
        if (termPlanStrand is null)
        {
            termPlanYearLevel!.AddStrand(strand);
            return;
        }

        var termPlanSubstrand = termPlanStrand!.HasSubstrands ? termPlanStrand!.Substrands!.FirstOrDefault(ss => ss.Name == substrand?.Name) : null;
        if (termPlanStrand.HasSubstrands && termPlanSubstrand is null)
        {
            termPlanStrand.AddSubstrand(substrand!);
            return;
        }

        var termPlanContentDescription = !termPlanStrand.HasSubstrands
            ? termPlanStrand.ContentDescriptions!.FirstOrDefault(cd => cd.CurriculumCode == contentDescription.CurriculumCode)
            : termPlanSubstrand!.ContentDescriptions.FirstOrDefault(cd => cd.CurriculumCode == contentDescription.CurriculumCode);

        if (termPlanContentDescription is null)
        {
            if (!termPlanStrand.HasSubstrands)
            {
                termPlanStrand.AddContentDescription(contentDescription);
            }
            else
            {
                termPlanSubstrand!.AddContentDescription(contentDescription);
            }
        }
    }

    public static TermPlan Create(TermPlanner termPlanner, int termNumber, List<Subject> subjects)
    {
        return new TermPlan(termPlanner, termNumber, subjects);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private TermPlan() { }
}
