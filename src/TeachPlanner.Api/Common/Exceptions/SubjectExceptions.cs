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

public class InvalidCurriculumSubjectIdException : BaseException
{
    public InvalidCurriculumSubjectIdException() :
        base("One of the subject IDs was not a curriculum subject", 404, "Subject.NotFound")
    {
    }
}

public class SubjectNotFoundException : BaseException
{
    public SubjectNotFoundException(string subjectName) : base($"No subject found with the name of {subjectName}", 404, "Subject.NotFound")
    {
    }
}