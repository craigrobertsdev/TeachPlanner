using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TeachPlanner.Shared.Domain.Curriculum;
using TeachPlanner.Shared.Domain.PlannerTemplates;
using TeachPlanner.Shared.Domain.Teachers;
using TeachPlanner.Shared.Contracts.Teachers.AccountSetup;
using TeachPlanner.Shared.Domain.Common.Enums;
using TeachPlanner.Shared.Common.Interfaces.Persistence;
using TeachPlanner.Shared.Common.Exceptions;

namespace TeachPlanner.Blazor.Features.Teachers;

public static class AccountSetup {
    public async static Task<IResult> Delegate([FromRoute] Guid teacherId, [FromBody] AccountSetupRequest request, [FromQuery] int calendarYear, ISender sender, Validator validator, CancellationToken cancellationToken) {
        var dayPlanTemplate = CreateDayPlanTemplate(request.DayPlanPattern, new TeacherId(teacherId));
        var yearLevelsTaught = request.YearLevelsTaught.Select(Enum.Parse<YearLevelValue>).ToList();
        var command = new Command(request.SubjectsTaught, yearLevelsTaught, dayPlanTemplate, new TeacherId(teacherId), calendarYear);

        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid) {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        await sender.Send(command);

        return Results.Ok();
    }

    public record Command(
        List<string> SubjectsTaught,
        List<YearLevelValue> YearLevelsTaught,
        DayPlanTemplate DayPlanTemplate,
        TeacherId TeacherId,
        int CalendarYear) : IRequest;

    public class Validator : AbstractValidator<Command> {
        public Validator() {
            RuleFor(x => x.SubjectsTaught).NotEmpty();
            RuleFor(x => x.DayPlanTemplate).NotNull();
        }
    }

    public sealed class Handler : IRequestHandler<Command> {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IYearDataRepository _yearDataRepository;
        private readonly ICurriculumRepository _curriculumRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(ITeacherRepository teacherRepository, ICurriculumRepository curriculumRepository, IYearDataRepository yearDataRepository, IUnitOfWork unitOfWork) {
            _teacherRepository = teacherRepository;
            _curriculumRepository = curriculumRepository;
            _yearDataRepository = yearDataRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(Command request, CancellationToken cancellationToken) {
            var teacher = await _teacherRepository.GetById(request.TeacherId, cancellationToken);

            if (teacher == null) {
                throw new TeacherNotFoundException();
            }

            var subjectsTaught = await _curriculumRepository.GetSubjectsByName(request.SubjectsTaught, cancellationToken);
            ValidateSubjects(subjectsTaught, request.SubjectsTaught);
            teacher.SetSubjectsTaught(subjectsTaught);

            await _yearDataRepository.SetInitialAccountDetails(teacher, request.YearLevelsTaught, request.DayPlanTemplate, request.CalendarYear, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        private void ValidateSubjects(List<CurriculumSubject> subjects, List<string> subjectNames) {
            if (subjects.Count != subjectNames.Count) {
                throw new SubjectNotFoundException(subjectNames.Except(subjects.Select(s => s.Name)).First());
            }
        }
    }

    private static DayPlanTemplate CreateDayPlanTemplate(DayPlanPatternDto dayPlanPattern, TeacherId teacherId) {
        var templatePeriods = CreateTemplatePeriods(dayPlanPattern);
        templatePeriods.Sort((x, y) => x.StartTime.CompareTo(y.StartTime));

        ValidateTemplatePeriodTimes(templatePeriods);

        return DayPlanTemplate.Create(templatePeriods, teacherId);
    }

    private static List<TemplatePeriod> CreateTemplatePeriods(DayPlanPatternDto dayPlanPattern) {
        List<TemplatePeriod> periods = new();

        for (int i = 0; i < dayPlanPattern.LessonTemplates.Count; i++) {
            periods.Add(new TemplatePeriod(
                PeriodType.Lesson,
                $"Lesson {i + 1}",
                CreateTimeFromDto(dayPlanPattern.LessonTemplates[i].StartTime),
                CreateTimeFromDto(dayPlanPattern.LessonTemplates[i].EndTime)));
        }

        for (int i = 0; i < dayPlanPattern.BreakTemplates.Count; i++) {
            periods.Add(new TemplatePeriod(
                PeriodType.Break,
                dayPlanPattern.BreakTemplates[i].Name,
                CreateTimeFromDto(dayPlanPattern.BreakTemplates[i].StartTime),
                CreateTimeFromDto(dayPlanPattern.BreakTemplates[i].EndTime)));
        }

        return periods;
    }

    private static TimeOnly CreateTimeFromDto(PeriodTimeDto periodTimeDto) {
        if (periodTimeDto.Minutes > 59 || periodTimeDto.Minutes < 0
            || periodTimeDto.Hours < 1 || periodTimeDto.Hours > 12) {
            throw new CreateTimeFromDtoException($"Time is out of range: {periodTimeDto.Hours}:{periodTimeDto.Minutes}${periodTimeDto.Period}");
        }

        if (periodTimeDto.Period == "AM" && periodTimeDto.Hours == 12) {
            return new TimeOnly(0, periodTimeDto.Minutes);
        }

        if (periodTimeDto.Period == "AM") {
            return new TimeOnly(periodTimeDto.Hours, periodTimeDto.Minutes);
        }

        if (periodTimeDto.Period == "PM" && periodTimeDto.Hours == 12) {
            return new TimeOnly(12, periodTimeDto.Minutes);
        }

        return new TimeOnly(periodTimeDto.Hours + 12, periodTimeDto.Minutes);
    }

    private static void ValidateTemplatePeriodTimes(List<TemplatePeriod> templatePeriods) {
        for (int i = 0; i < templatePeriods.Count - 1; i++) {
            if (templatePeriods[i].EndTime != templatePeriods[i + 1].StartTime) {
                throw new CreateTimeFromDtoException("Lesson and break start and end times must be continuous");
            }
        }
    }

    private static List<TermDate> ParseTermDates(List<TermDateDto> termDates) {
        return termDates.Select((td, i) => {
            var startDate = DateOnly.Parse(td.StartDate.Substring(0, 10));
            var endDate = DateOnly.Parse(td.EndDate.Substring(0, 10));

            return new TermDate(i + 1, startDate, endDate);
        }).ToList();
    }
}
