using TeachPlanner.Api.Contracts.Subjects;
using TeachPlanner.Api.Domain.LessonPlans;

namespace TeachPlanner.Api.Contracts.LessonPlans.CreateLessonPlan;

public record CreateLessonPlanRequest(
    Guid YearDataId,
    SubjectRequest Subject,
    string PlanningNotes,
    List<LessonPlanResource>? LessonPlanResources,
    List<Guid>? AssessmentIds,
    DateOnly LessonDate,
    int NumberOfPeriods,
    int StartPeriod);

