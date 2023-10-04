using TeachPlanner.Api.Domain.Teachers;
using TeachPlanner.Api.Domain.Users;

namespace TeachPlanner.WebApi.Tests.Helpers;
internal static class TeacherHelpers
{
    internal static Teacher CreateTeacher()
    {
        return Teacher.Create(new UserId(Guid.NewGuid()), "First", "Last");
    }
}
