using Domain.TeacherAggregate;

namespace Application.Common.Interfaces.Authentication;
public interface IJwtTokenGenerator
{
    string GenerateToken(Teacher teacher);
}
