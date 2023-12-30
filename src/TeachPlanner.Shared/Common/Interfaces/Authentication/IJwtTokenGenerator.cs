using TeachPlanner.Shared.Domain.Teachers;

namespace TeachPlanner.Shared.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator {
    string GenerateToken(Teacher teacher);
}