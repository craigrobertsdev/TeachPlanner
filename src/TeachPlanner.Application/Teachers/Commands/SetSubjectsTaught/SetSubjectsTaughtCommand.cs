using MediatR;

namespace TeachPlanner.Application.Teachers.Commands.SetSubjectsTaught;
public record SetSubjectsTaughtCommand(
    Guid TeacherId,
    List<Guid> SubjectIds
    ) : IRequest;
