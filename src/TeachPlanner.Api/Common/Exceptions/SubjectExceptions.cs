using TeachPlanner.Api.Domain.CurriculumSubjects;

namespace TeachPlanner.Api.Common.Exceptions;

public class StrandCreationException : BaseException
{
    public StrandCreationException() :
        base("Either substrands or contentDescriptions must be provided",
            400,
            "Subject.NeitherStrandNorSubstrand")
    {
    }
}

public class IsNonCurriculumSubjectException : BaseException
{
    public IsNonCurriculumSubjectException(CurriculumSubject subject) :
        base($"Subject: {subject.Name}, {subject.Id} is not a curriculum subject",
            400,
            "Subject.IsNonCurriculumSubject")
    {
    }
}

public class InvalidCurriculumSubjectIdException : BaseException
{
    public InvalidCurriculumSubjectIdException() :
        base("One of the subject IDs was not a curriculum subject", 404, "Subject.NotFound")
    {
    }
}