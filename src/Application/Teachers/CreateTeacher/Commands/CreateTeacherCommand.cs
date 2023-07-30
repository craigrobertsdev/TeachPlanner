using ErrorOr;
using MediatR;
using Domain.TeacherAggregate;
using Application.Teachers.Common;

namespace Application.Teachers.CreateTeacher.Commands;

public record CreateTeacherCommand(Guid UserId) : IRequest<ErrorOr<TeacherCreatedResult>>;
