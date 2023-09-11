using MediatR;
using TeachPlanner.Domain.LessonPlans;

namespace TeachPlanner.Application.LessonPlanners.Queries.GetLessonPlans;

public record GetLessonPlansQuery(Guid TeacherId) : IRequest<List<LessonPlan>>;
