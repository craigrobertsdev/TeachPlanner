using TeachPlanner.Shared.Domain.Teachers;

namespace TeachPlanner.Blazor.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator {
    string GenerateToken(Teacher teacher);
}