using TeachPlanner.Domain.Subjects;

namespace TeachPlanner.Domain.Common.Exceptions;
public class StrandCreationException : BaseException
{
    public StrandCreationException() :
        base("Either substrands or contentDescriptions must be provided",
        400,
        "Subject.NeitherStrandNorSubstrand")
    { }
}

public class IsNonCurriculumSubjectException : BaseException
{
    public IsNonCurriculumSubjectException(Subject subject) :
        base($"Subject: {subject.Name}, {subject.Id} is not a curriculum subject",
        400,
        "Subject.IsNonCurriculumSubject")
    { }
}
