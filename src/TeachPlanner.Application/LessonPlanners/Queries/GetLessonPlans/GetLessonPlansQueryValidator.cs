using FluentValidation;

namespace TeachPlanner.Application.LessonPlanners.Queries.GetLessonPlans;
public class GetLessonPlansQueryValidator : AbstractValidator<GetLessonPlansQuery>
{
    public GetLessonPlansQueryValidator()
    {
        RuleFor(x => x.TeacherId).NotEmpty();
    }
}
