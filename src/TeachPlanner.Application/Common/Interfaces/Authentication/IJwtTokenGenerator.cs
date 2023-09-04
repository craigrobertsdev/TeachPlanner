using TeachPlanner.Domain.Teachers;

namespace TeachPlanner.Application.Common.Interfaces.Authentication;
public interface IJwtTokenGenerator
{
    string GenerateToken(Teacher teacher);
}
