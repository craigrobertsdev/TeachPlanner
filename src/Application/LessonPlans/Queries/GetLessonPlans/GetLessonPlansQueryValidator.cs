using FluentValidation;

namespace TeachPlanner.Application.LessonPlans.Queries.GetLessonPlans;
public class GetLessonPlansQueryValidator : AbstractValidator<GetLessonPlansQuery>
{
    public GetLessonPlansQueryValidator()
    {
        RuleFor(x => x.TeacherId).NotEmpty();
    }
}
