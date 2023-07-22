﻿using FluentValidation;
using FluentValidation.Results;

namespace Application.LessonPlan.CreateLessonPlan.Commands;

public class CreateLessonPlanCommandValidator : AbstractValidator<CreateLessonPlanCommand>
{
    public CreateLessonPlanCommandValidator()
    {
        RuleFor(x => x.TeacherId).NotEmpty();
        RuleFor(x => x.SubjectId).NotEmpty();
        RuleFor(x => x.PlanningNotes).NotEmpty();
        RuleFor(x => x.StartTime).NotEmpty();
        RuleFor(x => x.EndTime).NotEmpty();
    }
}
