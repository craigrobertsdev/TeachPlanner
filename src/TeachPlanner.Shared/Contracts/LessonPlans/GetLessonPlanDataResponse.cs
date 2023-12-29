using TeachPlanner.Shared.Contracts.Curriculum;
using TeachPlanner.Shared.Contracts.Resources;

namespace TeachPlanner.Shared.Contracts.LessonPlans;

public record GetLessonPlanDataResponse(
    List<CurriculumSubjectDto> Curriculum,
    List<ResourceResponse> Resources);