using Domain.TeacherAggregate;

namespace Application.UnitTests.TestUtils.Constants;
public static partial class Constants
{
    public static class Teacher
    {
        public static TeacherId Id = new TeacherId(Guid.NewGuid());
    }
}
