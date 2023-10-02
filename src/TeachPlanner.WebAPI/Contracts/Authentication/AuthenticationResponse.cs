using TeachPlanner.Api.Domain.Teachers;

namespace TeachPlanner.Api.Contracts.Authentication;

public record AuthenticationResponse(Teacher Teacher, string Token);
