using TeachPlanner.Api.Entities.Teachers;

namespace TeachPlanner.Api.Contracts.Authentication;

public record AuthenticationResponse(Teacher Teacher, string Token);
