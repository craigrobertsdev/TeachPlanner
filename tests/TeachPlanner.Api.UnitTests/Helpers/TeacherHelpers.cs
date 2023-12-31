﻿using TeachPlanner.Api.Contracts.Teachers.AccountSetup;
using TeachPlanner.Api.Domain.Common.Enums;
using TeachPlanner.Api.Domain.CurriculumSubjects;
using TeachPlanner.Api.Domain.PlannerTemplates;
using TeachPlanner.Api.Domain.Teachers;
using TeachPlanner.Api.Domain.Users;

namespace TeachPlanner.Api.UnitTests.Helpers;
internal static class TeacherHelpers {
    internal static Teacher CreateTeacher() {
        return Teacher.Create(new UserId(Guid.NewGuid()), "First", "Last");
    }

    internal static List<string> CreateSubjectNames() {
        return new List<string>
        {
            "Mathematics",
            "English",
            "Science",
        };
    }

    internal static List<CurriculumSubject> CreateCurriculumSubjects() {
        return CreateSubjectNames().Select(subjectNames => CurriculumSubject.Create(subjectNames, new())).ToList();
    }

    internal static DayPlanPatternDto CreateDayPlanPatternDto() {
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

    internal static DayPlanPatternDto CreateDayPlanPatternDtoWithOverlappingTimes() {
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

    internal static List<TermDate> CreateTermDates() {
        return new List<TermDate>()
        {
            new TermDate(1, new DateOnly(2023, 1, 30), new DateOnly(2023, 4, 1)),
            new TermDate(2, new DateOnly(2023, 4, 15), new DateOnly(2023, 6, 30)),
            new TermDate(3, new DateOnly(2023, 7, 14), new DateOnly(2023, 9, 25)),
            new TermDate(4, new DateOnly(2023, 10, 10), new DateOnly(2023, 12, 15)),
        };
    }
    internal static List<TermDateDto> CreateTermDateDtos() {
        return new List<TermDateDto>()
        {
            new TermDateDto("2023-01-30", "2023-04-01"),
            new TermDateDto("2023-04-15", "2023-06-30"),
            new TermDateDto("2023-07-14", "2023-09-25"),
            new TermDateDto("2023-10-10", "2023-12-15")
        };
    }

    internal static List<TermDateDto> CreateTermDateDtosWithOverlappingDates() {
        return new List<TermDateDto>()
        {
            new TermDateDto("2023-01-30", "2023-04-01"),
            new TermDateDto("2023-04-15", "2023-06-30"),
            new TermDateDto("2023-07-14", "2023-09-25"),
            new TermDateDto("2023-10-10", "2023-12-15")
        };

    }
    internal static DayPlanTemplate CreateDayPlanTemplate(DayPlanPatternDto dayPlanPattern) {
        var periodTemplates = new List<TemplatePeriod>
        {
            new TemplatePeriod(PeriodType.Lesson, "Lesson 1", new TimeOnly(9, 10), new TimeOnly(10, 0)),
            new TemplatePeriod(PeriodType.Lesson, "Lesson 2", new TimeOnly(10, 0), new TimeOnly(10, 50)),
            new TemplatePeriod(PeriodType.Break, dayPlanPattern.BreakTemplates[0].Name, new TimeOnly(10, 50), new TimeOnly(11, 20)),
            new TemplatePeriod(PeriodType.Lesson, "Lesson 3", new TimeOnly(11, 20), new TimeOnly(12, 10)),
            new TemplatePeriod(PeriodType.Lesson, "Lesson 4", new TimeOnly(12, 10), new TimeOnly(13, 0)),
            new TemplatePeriod(PeriodType.Break, dayPlanPattern.BreakTemplates[1].Name, new TimeOnly(13, 0), new TimeOnly(13, 30)),
            new TemplatePeriod(PeriodType.Lesson, "Lesson 5", new TimeOnly(13, 30), new TimeOnly(14, 20)),
            new TemplatePeriod(PeriodType.Lesson, "Lesson 6", new TimeOnly(14, 20), new TimeOnly(15, 10))
        };

        return DayPlanTemplate.Create(periodTemplates, new TeacherId(Guid.NewGuid()));
    }

    internal static AccountSetupRequest CreateAccountSetupRequest() {
        return new AccountSetupRequest(CreateSubjectNames(), CreateYearLevelsTaughtAsStringList(), CreateDayPlanPatternDto());
    }
    internal static AccountSetupRequest CreateAccountSetupRequestWithOverlappingTimes() {
        return new AccountSetupRequest(CreateSubjectNames(), CreateYearLevelsTaughtAsStringList(), CreateDayPlanPatternDtoWithOverlappingTimes());
    }

    internal static AccountSetupRequest CreateAccountSetupRequestWithOverlappingDates() {
        return new AccountSetupRequest(CreateSubjectNames(), CreateYearLevelsTaughtAsStringList(), CreateDayPlanPatternDto());
    }

    internal static List<CurriculumSubject> CreateCurriculumSubjects(List<string> subjectNames) {
        return subjectNames.Select(subjectNames => CurriculumSubject.Create(subjectNames, new())).ToList();
    }

    internal static List<string> CreateYearLevelsTaughtAsStringList() {
        return new List<string> { "Year1", "Year2" };
    }

    internal static List<YearLevelValue> CreateYearLevelsTaught() {
        return new List<YearLevelValue> { YearLevelValue.Year1, YearLevelValue.Year2 };
    }
}
