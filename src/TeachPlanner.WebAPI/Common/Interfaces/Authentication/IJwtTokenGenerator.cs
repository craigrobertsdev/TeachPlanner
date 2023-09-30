using TeachPlanner.Api.Entities.Teachers;

namespace TeachPlanner.Api.Common.Interfaces.Authentication;
public interface IJwtTokenGenerator
{
    string GenerateToken(Teacher teacher);
}
