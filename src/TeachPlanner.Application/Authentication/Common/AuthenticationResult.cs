using TeachPlanner.Domain.Teachers;

namespace TeachPlanner.Application.Authentication.Common;

public record AuthenticationResult(Teacher Teacher, string Token);

