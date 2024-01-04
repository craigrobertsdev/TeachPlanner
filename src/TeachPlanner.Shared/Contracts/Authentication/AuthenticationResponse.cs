using TeachPlanner.Shared.Contracts.Teachers;

namespace TeachPlanner.Shared.Contracts.Authentication;

public record AuthenticationResponse(TeacherModel Teacher, string Token);