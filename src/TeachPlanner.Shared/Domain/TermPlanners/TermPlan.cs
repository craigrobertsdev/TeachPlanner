using TeachPlanner.Shared.Common.Exceptions;
using TeachPlanner.Shared.Domain.Curriculum;

namespace TeachPlanner.Shared.Domain.TermPlanners;

public record TermPlan {
    private readonly List<CurriculumSubject> _subjects = new();

    private TermPlan(TermPlanner termPlanner, int termNumber, List<CurriculumSubject> subjects) {
        TermPlanner = termPlanner;
        TermNumber = termNumber;
        _subjects = subjects;
    }

    public IReadOnlyList<CurriculumSubject> Subjects => _subjects.AsReadOnly();
    public TermPlanner TermPlanner { get; private set; } = null!;
    public int TermNumber { get; private set; }

    public void AddSubject(CurriculumSubject subject) {
        if (!_subjects.Contains(subject)) _subjects.Add(subject);
    }

    public void AddSubjects(List<CurriculumSubject> subjects) {
        if (_subjects.Count > 0) throw new TermPlanSubjectsAlreadySetException();

        _subjects.AddRange(subjects);
    }

    public void SetSubjectAtIndex(CurriculumSubject subject, int index) {
        _subjects[index] = subject;
    }

    public void UpdateSubject(CurriculumSubject subject) {
        var subjectToUpdate = _subjects.FirstOrDefault(s => s.Id == subject.Id);

        if (subjectToUpdate is null) {
            _subjects.Add(subject);
            return;
        }

        // Find the difference between the two subjects and add the new content descriptions
        var yearLevel = subject.YearLevels[0];
        var yearLevelToUpdate = subjectToUpdate.YearLevels.FirstOrDefault(yl => yl.Name == yearLevel.Name);

        if (yearLevelToUpdate is null) {
            subject.AddYearLevel(yearLevel);
        }
        // TODO:
    }

    public static TermPlan Create(TermPlanner termPlanner, int termNumber, List<CurriculumSubject> subjects) {
        return new TermPlan(termPlanner, termNumber, subjects);
    }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private TermPlan() {
    }
}