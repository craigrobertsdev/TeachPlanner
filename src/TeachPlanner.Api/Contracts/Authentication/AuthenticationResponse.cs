using TeachPlanner.Api.Contracts.Teachers;

namespace TeachPlanner.Api.Contracts.Authentication;

public record AuthenticationResponse(TeacherResponse Teacher, string Token);
