using ErrorOr;
using MediatR;

namespace TeachPlanner.Application.Teachers.Commands.SetSubjectsTaught;
public record SetSubjectsTaughtCommand(
    Guid TeacherId,
    List<string> SubjectNames
    ) : IRequest<ErrorOr<SetSubjectsTaughtResult>>;
