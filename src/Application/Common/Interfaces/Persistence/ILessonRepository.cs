namespace Application.Common.Interfaces.Persistence;

public interface ILessonRepository
{
    Task Create(Domain.LessonPlanAggregate.LessonPlan lesson);
}
