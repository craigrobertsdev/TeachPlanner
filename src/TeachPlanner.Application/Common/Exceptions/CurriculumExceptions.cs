using TeachPlanner.Domain.Common.Exceptions;

namespace TeachPlanner.Application.Common.Exceptions;
public class AttemptedToAddNonCurriculumSubjectException : BaseException
{
    public AttemptedToAddNonCurriculumSubjectException(string subjectName)
        : base($"Cannot add non-curriculum subjects when parsing the curriculum. Subject name: {subjectName}",
            400, "Curriculum.AddNonCurriculumSubject")
    { }
}
