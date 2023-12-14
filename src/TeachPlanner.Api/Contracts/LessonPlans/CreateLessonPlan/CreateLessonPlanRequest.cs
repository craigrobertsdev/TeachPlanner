using TeachPlanner.Api.Domain.CurriculumSubjects;
using TeachPlanner.Api.Domain.LessonPlans;

namespace TeachPlanner.Api.Contracts.LessonPlans.CreateLessonPlan;

public record CreateLessonPlanRequest(
    SubjectId SubjectId,
    List<string> CurriculumCodes,
    string PlanningNotes,
    List<LessonPlanResource>? LessonPlanResources,
    List<Guid>? AssessmentIds,
    DateOnly LessonDate,
    int NumberOfPeriods,
    int StartPeriod);