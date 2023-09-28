using MediatR;

namespace TeachPlanner.Application.YearDataRecords.Commands.SetSubjectsTaught;
public record SetSubjectsTaughtCommand(
    Guid TeacherId,
    List<Guid> SubjectIds,
    int CalendarYear
    ) : IRequest;
