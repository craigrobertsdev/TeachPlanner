using TeachPlanner.Shared.Domain.Teachers;
using TeachPlanner.Shared.Models.Authentication;

namespace TeachPlanner.Shared.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator {
    TokenResponse GenerateToken(Teacher teacher);
}