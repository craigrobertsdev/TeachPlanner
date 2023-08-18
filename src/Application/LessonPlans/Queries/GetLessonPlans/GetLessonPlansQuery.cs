using Domain.LessonPlanAggregate;
using ErrorOr;
using MediatR;

namespace Application.LessonPlans.Queries.GetLessonPlans;

public record GetLessonPlansQuery(Guid TeacherId) : IRequest<ErrorOr<List<LessonPlan>>>;
