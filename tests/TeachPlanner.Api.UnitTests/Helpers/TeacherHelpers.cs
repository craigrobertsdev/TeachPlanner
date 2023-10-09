using TeachPlanner.Api.Domain.Teachers;
using TeachPlanner.Api.Domain.Users;

namespace TeachPlanner.Api.UnitTests.Helpers;
internal static class TeacherHelpers
{
    internal static Teacher CreateTeacher()
    {
        return Teacher.Create(new UserId(Guid.NewGuid()), "First", "Last");
    }
}
