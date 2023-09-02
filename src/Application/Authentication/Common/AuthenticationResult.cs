using TeachPlanner.Domain.Teacher;

namespace TeachPlanner.Application.Authentication.Common;

public record AuthenticationResult(Teacher Teacher, string Token);

