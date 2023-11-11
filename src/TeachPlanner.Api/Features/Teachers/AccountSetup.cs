using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TeachPlanner.Api.Common.Exceptions;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Contracts.Teachers.AccountSetup;
using TeachPlanner.Api.Domain.PlannerTemplates;
using TeachPlanner.Api.Domain.Teachers;

namespace TeachPlanner.Api.Features.Teachers;

public static class AccountSetup
{
    public async static Task<IResult> Delegate([FromRoute] Guid teacherId, [FromBody] AccountSetupRequest request, ISender sender, Validator validator) {
        var termDates = request.TermDates.Select(td => new TermDate(td.StartDate, td.EndDate)).ToList();
        var dayPlanTemplate = CreateDayPlanTemplate(request.DayPlanPattern);
        var command = new Command(request.SubjectsTaught, dayPlanTemplate, termDates, new TeacherId(teacherId));

        var validationResult = await validator.ValidateAsync(command);

        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        await sender.Send(command);

        return Results.Ok();
    }

    public record Command(List<string> SubjectsTaught, DayPlanTemplate DayPlanTemplate, List<TermDate> TermDates, TeacherId TeacherId): IRequest;

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.SubjectsTaught).NotEmpty();
            RuleFor(x => x.DayPlanTemplate).NotNull();
            RuleFor(x => x.TermDates.Count).Equals(4);
        }
    }

    public sealed class Handler : IRequestHandler<Command>
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(ITeacherRepository teacherRepository, IUnitOfWork unitOfWork)
        {
            _teacherRepository = teacherRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var teacher = await _teacherRepository.GetById(request.TeacherId, cancellationToken);

            if (teacher == null)
            {
                throw new TeacherNotFoundException();
            }

            _teacherRepository.SetInitialAccountDetails(teacher.Id, request.SubjectsTaught, request.DayPlanTemplate, request.TermDates, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

    }
    private static DayPlanTemplate CreateDayPlanTemplate(DayPlanPatternDto dayPlanPattern)
    {
        var templatePeriods = CreateTemplatePeriods(dayPlanPattern);
        templatePeriods.Sort((x, y) => x.StartTime.CompareTo(y.StartTime));

        ValidateTemplatePeriodTimes(templatePeriods);

        return DayPlanTemplate.Create(templatePeriods);
    }

    private static List<TemplatePeriod> CreateTemplatePeriods(DayPlanPatternDto dayPlanPattern)
    {
        List<TemplatePeriod> periods = new();

        for (int i = 0; i < dayPlanPattern.LessonTemplates.Count; i++)
        {
            periods.Add(new TemplatePeriod(
                PeriodType.Lesson,
                $"Lesson {i + 1}",
                CreateTimeFromDto(dayPlanPattern.LessonTemplates[i].StartTime),
                CreateTimeFromDto(dayPlanPattern.LessonTemplates[i].EndTime)));
        } 

        for (int i = 0; i < dayPlanPattern.BreakTemplates.Count; i++)
        {
            periods.Add(new TemplatePeriod(
                PeriodType.Break,
                dayPlanPattern.BreakTemplates[i].Name,
                CreateTimeFromDto(dayPlanPattern.BreakTemplates[i].StartTime),
                CreateTimeFromDto(dayPlanPattern.BreakTemplates[i].EndTime)));
        }

        return periods;
    }

    private static TimeOnly CreateTimeFromDto(PeriodTimeDto periodTimeDto)
    {
        if (periodTimeDto.Minutes > 59 || periodTimeDto.Minutes < 0 
            || periodTimeDto.Hours < 1 || periodTimeDto.Hours > 12)
        {
            throw new CreateTimeFromDtoException($"Time is out of range: {periodTimeDto.Hours}:{periodTimeDto.Minutes}${periodTimeDto.Period}");
        }

        if (periodTimeDto.Period == "AM" && periodTimeDto.Hours == 12)
        {
            return new TimeOnly(0, periodTimeDto.Minutes);
        }

        if (periodTimeDto.Period == "AM")
        {
            return new TimeOnly(periodTimeDto.Hours, periodTimeDto.Minutes);
        }
        
        if (periodTimeDto.Period == "PM" && periodTimeDto.Hours == 12)
        {
            return new TimeOnly(12, periodTimeDto.Minutes);
        }
        
        return new TimeOnly(periodTimeDto.Hours + 12, periodTimeDto.Minutes);
    }

    private static void ValidateTemplatePeriodTimes(List<TemplatePeriod> templatePeriods)
    {
        for (int i = 0; i < templatePeriods.Count - 1; i++)
        {
            if (templatePeriods[i].EndTime != templatePeriods[i + 1].StartTime)
            {
                throw new CreateTimeFromDtoException("Lesson and break start and end times must be continuous");
            }
        }
    }
}
