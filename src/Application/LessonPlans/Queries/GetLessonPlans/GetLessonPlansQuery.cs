using ErrorOr;
using MediatR;
using TeachPlanner.Domain.LessonPlans;

namespace TeachPlanner.Application.LessonPlans.Queries.GetLessonPlans;

public record GetLessonPlansQuery(Guid TeacherId) : IRequest<ErrorOr<List<LessonPlan>>>;
