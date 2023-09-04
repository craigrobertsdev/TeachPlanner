using ErrorOr;
using MediatR;
using TeachPlanner.Domain.LessonPlans;
using TeachPlanner.Domain.Resources;

namespace TeachPlanner.Application.LessonPlans.CreateLessonPlan.Commands;

public record CreateLessonPlanCommand(
    string TeacherId,
    string SubjectId,
    string PlanningNotes,
    List<Resource>? Resources,
    List<string> SummativeAssessmentIds,
    List<string> FormativeAssessmentIds,
    DateTime StartTime,
    DateTime EndTime,
    int NumberOfPeriods) : IRequest<ErrorOr<LessonPlan>>;
