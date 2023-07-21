using Application.LessonPlan.CreateLessonPlan.Common;
using ErrorOr;
using MediatR;

namespace Application.LessonPlan.CreateLessonPlan.Commands;

public record CreateLessonPlanCommand(
    string TeacherId,
    string SubjectId,
    string PlanningNotes,
    DateTime StartTime,
    DateTime EndTime,
    List<CreateResourceCommand>? Resources = null,
    List<CreateAssessmentCommand>? Assessments = null
    ) : IRequest<ErrorOr<CreateLessonPlanResult>>;

public record class CreateResourceCommand(
    string Name,
    string Url);

public record CreateAssessmentCommand(
    string Name,
    string Url);
