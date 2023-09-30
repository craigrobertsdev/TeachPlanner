using TeachPlanner.Api.Entities.Subjects;

namespace TeachPlanner.Api.Common.Exceptions;
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

public class SubjectNotFoundException : BaseException
{
    public SubjectNotFoundException() :
        base("One of the subjects was not found", 404, "Subject.NotFound")
    { }
}
