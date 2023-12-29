using TeachPlanner.Shared.Domain.Curriculum;
using TeachPlanner.Shared.Domain.LessonPlans;

namespace TeachPlanner.Shared.Contracts.LessonPlans.CreateLessonPlan;

public record CreateLessonPlanRequest(
    SubjectId SubjectId,
    List<string> CurriculumCodes,
    string PlanningNotes,
    List<LessonPlanResource>? LessonPlanResources,
    List<Guid>? AssessmentIds,
    DateOnly LessonDate,
    int NumberOfPeriods,
    int StartPeriod);