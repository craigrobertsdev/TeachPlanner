using MediatR;
using TeachPlanner.Api.Domain.Subjects;
using TeachPlanner.Api.Domain.Teachers;

namespace TeachPlanner.Api.Features.YearDataRecords.Commands.SetSubjectsTaught;
public record SetSubjectsTaughtCommand(
    TeacherId TeacherId,
    List<SubjectId> SubjectIds,
    int CalendarYear
    ) : IRequest;
