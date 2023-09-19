using MediatR;
using TeachPlanner.Domain.Resources;

namespace TeachPlanner.Application.LessonPlanners.Commands.CreateLessonPlan;

public record CreateLessonPlanCommand(
    string TeacherId,
    string SubjectId,
    string PlanningNotes,
    List<Resource>? Resources,
    List<string> SummativeAssessmentIds,
    List<string> FormativeAssessmentIds,
    DateTime StartTime,
    DateTime EndTime,
    int NumberOfPeriods) : IRequest;
