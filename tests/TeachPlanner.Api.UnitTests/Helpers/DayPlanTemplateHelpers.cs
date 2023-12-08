using TeachPlanner.Api.Domain.PlannerTemplates;
using TeachPlanner.Api.Domain.Teachers;

namespace TeachPlanner.Api.UnitTests.Helpers;

public static class DayPlanTemplateHelpers {
  public static DayPlanTemplate CreateDayPlanTemplate(TeacherId? teacherId = null) {
    if (teacherId is null) {
      teacherId = new TeacherId(Guid.NewGuid());
    }
    return DayPlanTemplate.Create(
      new List<TemplatePeriod> {
        new TemplatePeriod(PeriodType.Lesson, string.Empty, new TimeOnly(9, 10 ,0), new TimeOnly(10, 0, 0)),
        new TemplatePeriod(PeriodType.Lesson, string.Empty, new TimeOnly(10, 0 ,0), new TimeOnly(10, 50, 0)),
        new TemplatePeriod(PeriodType.Break, "Recess", new TimeOnly(10, 50, 0), new TimeOnly(11, 20, 0)),
        new TemplatePeriod(PeriodType.Lesson, string.Empty, new TimeOnly(11, 20, 0), new TimeOnly(12, 10, 0)),
        new TemplatePeriod(PeriodType.Lesson, string.Empty, new TimeOnly(12, 10, 0), new TimeOnly(13, 0, 0)),
        new TemplatePeriod(PeriodType.Break, "Lunch", new TimeOnly(13, 0, 0), new TimeOnly(13, 30, 0)),
        new TemplatePeriod(PeriodType.Lesson, string.Empty, new TimeOnly(13, 30, 0), new TimeOnly(14, 20, 0)),
        new TemplatePeriod(PeriodType.Lesson, string.Empty, new TimeOnly(14, 20, 0), new TimeOnly(15, 10, 0)),
      },
      teacherId
    );
  }
}