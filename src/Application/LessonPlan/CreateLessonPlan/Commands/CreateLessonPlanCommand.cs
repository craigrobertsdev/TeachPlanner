using Domain.LessonAggregate;
using ErrorOr;
using MediatR;

namespace Application.LessonPlan.CreateLessonPlan.Commands;

public record CreateLessonPlanCommand(
    string TeacherId,
    string SubjectId,
    string PlanningNotes,
    List<string>? ResourceIds,
    List<string>? AssessmentIds,
    DateTime StartTime,
    DateTime EndTime) : IRequest<ErrorOr<Lesson>>;
