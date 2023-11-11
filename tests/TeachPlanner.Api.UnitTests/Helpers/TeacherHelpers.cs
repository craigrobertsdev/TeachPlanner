using TeachPlanner.Api.Contracts.Teachers.AccountSetup;
using TeachPlanner.Api.Domain.PlannerTemplates;
using TeachPlanner.Api.Domain.Teachers;
using TeachPlanner.Api.Domain.Users;

namespace TeachPlanner.Api.UnitTests.Helpers;
internal static class TeacherHelpers
{
    internal static Teacher CreateTeacher()
    {
        return Teacher.Create(new UserId(Guid.NewGuid()), "First", "Last");
    }

    internal static List<string> CreateSubjectsTaught()
    {
        return new List<string>
        {
            "Maths",
            "English",
            "Science",
        };
    }

    internal static DayPlanPatternDto CreateDayPlanPatternDto()
    {
        return new DayPlanPatternDto(
             new List<LessonTemplateDto>
             {
                 new LessonTemplateDto(
                     new PeriodTimeDto(9, 10, "AM"),
                     new PeriodTimeDto(10, 0, "AM")),
                 new LessonTemplateDto(
                     new PeriodTimeDto(10, 0, "AM"),
                     new PeriodTimeDto(10, 50, "AM")),
                 new LessonTemplateDto(
                     new PeriodTimeDto(11, 20, "AM"),
                     new PeriodTimeDto(12, 10, "PM")),
                 new LessonTemplateDto(
                     new PeriodTimeDto(12, 10, "PM"),
                     new PeriodTimeDto(1, 0, "PM")),
                 new LessonTemplateDto(
                     new PeriodTimeDto(1, 30, "PM"),
                     new PeriodTimeDto(2, 20, "PM")),
                 new LessonTemplateDto(
                     new PeriodTimeDto(2, 20, "PM"),
                     new PeriodTimeDto(3, 10, "PM"))
             },
             new List<BreakTemplateDto>
             {
                 new BreakTemplateDto(
                     "Recess",
                     new PeriodTimeDto(10, 50, "AM"),
                     new PeriodTimeDto(11, 20, "AM")),
                 new BreakTemplateDto(
                     "Lunch",
                     new PeriodTimeDto(1, 0, "PM"),
                     new PeriodTimeDto(1, 30, "PM"))
             });
    }

    internal static DayPlanPatternDto CreateDayPlanPatternDtoWithOverlappingTimes()
    {
        return new DayPlanPatternDto(
         new List<LessonTemplateDto>
         {
             new LessonTemplateDto(
                 new PeriodTimeDto(9, 10, "AM"),
                 new PeriodTimeDto(10, 0, "AM")),
             new LessonTemplateDto(
                 new PeriodTimeDto(10, 0, "AM"),
                 new PeriodTimeDto(10, 50, "AM")),
             new LessonTemplateDto(
                 new PeriodTimeDto(11, 20, "AM"),
                 new PeriodTimeDto(12, 10, "PM")),
             new LessonTemplateDto(
                 new PeriodTimeDto(12, 10, "PM"),
                 new PeriodTimeDto(1, 0, "PM")),
             new LessonTemplateDto(
                 new PeriodTimeDto(1, 30, "PM"),
                 new PeriodTimeDto(2, 20, "PM")),
             new LessonTemplateDto(
                 new PeriodTimeDto(2, 20, "PM"),
                 new PeriodTimeDto(3, 10, "PM"))
         },
         new List<BreakTemplateDto>
         {
             new BreakTemplateDto(
                 "Recess",
                 new PeriodTimeDto(10, 50, "AM"),
                 new PeriodTimeDto(11, 20, "AM")),
             new BreakTemplateDto(
                 "Lunch",
                 new PeriodTimeDto(1, 0, "PM"),
                 new PeriodTimeDto(1, 30, "PM"))
         });
    }

    internal static List<TermDate> CreateTermDates()
    {
        return new List<TermDate>()
        {
            new TermDate(new DateOnly(2023, 1, 30), new DateOnly(2023, 4, 1)),
            new TermDate(new DateOnly(2023, 4, 15), new DateOnly(2023, 6, 30)),
            new TermDate(new DateOnly(2023, 7, 14), new DateOnly(2023, 9, 25)),
            new TermDate(new DateOnly(2023, 10, 10), new DateOnly(2023, 12, 15)),
        };
    }
    internal static List<TermDateDto> CreateTermDateDtos()
    {
        return new List<TermDateDto>()
        {
            new TermDateDto(new DateOnly(2023, 1, 30), new DateOnly(2023, 4, 1)),
            new TermDateDto(new DateOnly(2023, 4, 15), new DateOnly(2023, 6, 30)),
            new TermDateDto(new DateOnly(2023, 7, 14), new DateOnly(2023, 9, 25)),
            new TermDateDto(new DateOnly(2023, 10, 10), new DateOnly(2023, 12, 15)),
        };
    }

    internal static List<TermDateDto>CreateTermDateDtosWithOverlappingDates() { 
        return new List<TermDateDto>()
        {
            new TermDateDto(new DateOnly(2023, 1, 30), new DateOnly(2023, 4, 1)),
            new TermDateDto(new DateOnly(2023, 3, 15), new DateOnly(2023, 6, 30)),
            new TermDateDto(new DateOnly(2023, 7, 14), new DateOnly(2023, 9, 25)),
            new TermDateDto(new DateOnly(2023, 10, 10), new DateOnly(2023, 12, 15)),
        };

    }
    internal static DayPlanTemplate CreateDayPlanTemplate(DayPlanPatternDto dayPlanPattern)
    {
        var periodTemplates = new List<TemplatePeriod>
        {
            new TemplatePeriod(PeriodType.Lesson, "Lesson 1", new TimeOnly(9, 10), new TimeOnly(10, 0)),
            new TemplatePeriod(PeriodType.Lesson, "Lesson 2", new TimeOnly(10, 0), new TimeOnly(10, 50)),
            new TemplatePeriod(PeriodType.Break, dayPlanPattern.BreakTemplates[0].Name, new TimeOnly(10, 50), new TimeOnly(11, 20)),
            new TemplatePeriod(PeriodType.Lesson, "Lesson 3", new TimeOnly(11, 20), new TimeOnly(12, 10)),
            new TemplatePeriod(PeriodType.Lesson, "Lesson 4", new TimeOnly(12, 10), new TimeOnly(1, 0)),
            new TemplatePeriod(PeriodType.Break, dayPlanPattern.BreakTemplates[1].Name, new TimeOnly(1, 0), new TimeOnly(1, 30)),
            new TemplatePeriod(PeriodType.Lesson, "Lesson 5", new TimeOnly(1, 30), new TimeOnly(2, 20)),
            new TemplatePeriod(PeriodType.Lesson, "Lesson 6", new TimeOnly(2, 20), new TimeOnly(3, 10))
        };

        return DayPlanTemplate.Create(periodTemplates);
    }

    internal static AccountSetupRequest CreateAccountSetupRequest()
    {
        return new AccountSetupRequest(CreateSubjectsTaught(), CreateDayPlanPatternDto(), CreateTermDateDtos());
    }
    internal static AccountSetupRequest CreateAccountSetupRequestWithOverlappingTimes()
    {
        return new AccountSetupRequest(CreateSubjectsTaught(), CreateDayPlanPatternDtoWithOverlappingTimes(), CreateTermDateDtos());
    }

    internal static AccountSetupRequest CreateAccountSetupRequestWithOverlappingDates()
    {
        return new AccountSetupRequest(CreateSubjectsTaught(), CreateDayPlanPatternDto(), CreateTermDateDtosWithOverlappingDates());
    }
}
