using TeachPlanner.Domain.Common.Enums;
using TeachPlanner.Domain.Subjects;
using TeachPlanner.Domain.Teachers;

namespace TeachPlanner.Domain.Tests.Teachers;
internal static class TeacherHelpers
{
    internal static Teacher CreateTeacher()
    {
        return Teacher.Create(Guid.NewGuid(), "First", "Last");
    }
}
