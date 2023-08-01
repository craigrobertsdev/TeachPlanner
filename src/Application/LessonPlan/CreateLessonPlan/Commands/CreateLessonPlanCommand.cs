using Domain.LessonPlanAggregate;
using ErrorOr;
using MediatR;

namespace Application.LessonPlan.CreateLessonPlan.Commands;

public record CreateLessonPlanCommand(
    string TeacherId,
    string SubjectId,
    string PlanningNotes,
    List<string> ResourceIds,
    List<string> SummativeAssessmentIds,
    List<string> FormativeAssessmentIds,
    DateTime StartTime,
    DateTime EndTime) : IRequest<ErrorOr<Domain.LessonPlanAggregate.LessonPlan>>;
