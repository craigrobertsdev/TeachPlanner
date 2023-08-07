using Domain.LessonPlanAggregate;
using Domain.ResourceAggregate;
using ErrorOr;
using MediatR;

namespace Application.LessonPlan.CreateLessonPlan.Commands;

public record CreateLessonPlanCommand(
    string TeacherId,
    string SubjectId,
    string PlanningNotes,
    List<Resource> Resources,
    List<string> SummativeAssessmentIds,
    List<string> FormativeAssessmentIds,
    DateTime StartTime,
    DateTime EndTime) : IRequest<ErrorOr<Domain.LessonPlanAggregate.LessonPlan>>;
