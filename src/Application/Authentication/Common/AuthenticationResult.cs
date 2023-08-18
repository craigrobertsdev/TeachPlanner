using Domain.TeacherAggregate;

namespace Application.Authentication.Common;

public record AuthenticationResult(Teacher Teacher, string Token);

