using MediatR;
using TeachPlanner.Api.Entities.Resources;

namespace TeachPlanner.Api.Features.LessonPlanners.Commands.CreateLessonPlan;

public record CreateLessonPlanCommand(
    Guid TeacherId,
    Guid SubjectId,
    string PlanningNotes,
    List<Resource>? Resources,
    List<Guid> AssessmentIds,
    DateTime StartTime,
    DateTime EndTime,
    int NumberOfPeriods) : IRequest;
