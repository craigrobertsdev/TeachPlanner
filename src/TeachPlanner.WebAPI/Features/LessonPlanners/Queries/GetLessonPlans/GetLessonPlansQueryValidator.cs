using FluentValidation;

namespace TeachPlanner.Api.Features.LessonPlanners.Queries.GetLessonPlans;
public class GetLessonPlansQueryValidator : AbstractValidator<GetLessonPlansQuery>
{
    public GetLessonPlansQueryValidator()
    {
        RuleFor(x => x.TeacherId).NotEmpty();
    }
}
