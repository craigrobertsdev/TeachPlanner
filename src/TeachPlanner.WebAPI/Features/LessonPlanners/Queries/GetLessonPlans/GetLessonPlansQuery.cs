using MediatR;
using TeachPlanner.Api.Entities.LessonPlans;
using TeachPlanner.Api.Entities.Teachers;

namespace TeachPlanner.Api.Features.LessonPlanners.Queries.GetLessonPlans;

public record GetLessonPlansQuery(TeacherId TeacherId) : IRequest<List<LessonPlan>>;
