using ErrorOr;
using MediatR;
using TeachPlanner.Application.Teachers.Common;

namespace TeachPlanner.Application.Teachers.Commands.CreateTeacher;

public record CreateTeacherCommand(
  Guid TeacherId,
  string FirstName,
  string LastName,
  string Email,
  string Password) : IRequest<ErrorOr<TeacherCreatedResult>>;
