using ErrorOr;
using MediatR;
using Application.Teachers.Common;

namespace Application.Teachers.CreateTeacher.Commands;

public record CreateTeacherCommand(Guid UserId) : IRequest<ErrorOr<TeacherCreatedResult>>;
