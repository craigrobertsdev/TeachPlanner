using ErrorOr;
using MediatR;
using Application.Teachers.Common;

namespace Application.Teachers.CreateTeacher.Commands;

public record CreateTeacherCommand(
  Guid TeacherId,
  string FirstName,
  string LastName,
  string Email,
  string Password) : IRequest<ErrorOr<TeacherCreatedResult>>;
