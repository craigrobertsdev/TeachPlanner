using TeachPlanner.Shared.Domain.Common.Enums;
using TeachPlanner.Shared.Contracts.LessonPlans;
using TeachPlanner.Shared.Contracts.TermPlanners;
using TeachPlanner.Shared.Domain.LessonPlans;
using TeachPlanner.Shared.Domain.Students;
using TeachPlanner.Shared.Domain.TermPlanners;
using TeachPlanner.Shared.Domain.YearDataRecords;

namespace TeachPlanner.Shared.Contracts.Teachers.GetTeacherSettings;

public record GetTeacherSettingsResponse {
    public GetTeacherSettingsResponse(
        YearDataId yearDataId,
        IEnumerable<Subject> subjects,
        IEnumerable<Student> students,
        IEnumerable<YearLevelValue> yearLevelsTaught,
        TermPlanner? termPlanner) {
        YearDataId = yearDataId.Value;
        Subjects = subjects.ToList();
        Students = students.Select(s => new SettingsStudentResponse(s.FirstName, s.LastName)).ToList();
        YearLevelsTaught = yearLevelsTaught.ToList();
        TermPlanner = termPlanner != null ? TermPlannerResponse.Create(termPlanner) : null;
    }

    public Guid YearDataId { get; }
    public List<Subject> Subjects { get; }
    public List<SettingsStudentResponse> Students { get; }
    public List<YearLevelValue> YearLevelsTaught { get; }
    public TermPlannerResponse? TermPlanner { get; }
}

public record SettingsStudentResponse(
    string FirstName,
    string LastName);

public record SettingsLessonPlanResponse(
    Guid LessonPlanId,
    Guid SubjectId,
    string PlanningNotes,
    List<LessonCommentResponse> Comments,
    List<Guid> Resources,
    DateOnly LessonDate,
    int NumberOfPeriods,
    int StartPeriod) {
    public static List<SettingsLessonPlanResponse> CreateMany(IEnumerable<LessonPlan> lessonPlans) {
        return lessonPlans.Select(lp => new SettingsLessonPlanResponse(
            lp.Id.Value,
            lp.SubjectId.Value,
            lp.PlanningNotes,
            lp.Comments.Select(c => new LessonCommentResponse(
                c.Content,
                c.Completed,
                c.StruckOut,
                c.CompletedDateTime)).ToList(),
            lp.LessonPlanResources.Select(r => r.ResourceId.Value).ToList(),
            lp.LessonDate,
            lp.NumberOfPeriods,
            lp.StartPeriod)).ToList();
    }
}

// public record SettingsWeekPlannerResponse(
//     List<SettingsLessonPlanResponse> LessonPlans,
//     List<SchoolEventResponse> SchoolEvents,
//     DateTime WeekStart,
//     int WeekNumber)
// {
//     public static List<SettingsWeekPlannerResponse> CreateMany(IEnumerable<WeekPlanner> weekPlanners)
//     {
//         return weekPlanners.Select(wp => new SettingsWeekPlannerResponse(
//             SettingsLessonPlanResponse.CreateMany(wp.LessonPlans),
//             SchoolEventResponse.CreateMany(wp.SchoolEvents),
//             wp.WeekStart,
//             wp.WeekNumber)).ToList();
//     }
// }