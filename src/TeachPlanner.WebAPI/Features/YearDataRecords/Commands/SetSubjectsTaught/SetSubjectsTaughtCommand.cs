using MediatR;
using TeachPlanner.Api.Entities.Subjects;
using TeachPlanner.Api.Entities.Teachers;

namespace TeachPlanner.Api.Features.YearDataRecords.Commands.SetSubjectsTaught;
public record SetSubjectsTaughtCommand(
    TeacherId TeacherId,
    List<SubjectId> SubjectIds,
    int CalendarYear
    ) : IRequest;
