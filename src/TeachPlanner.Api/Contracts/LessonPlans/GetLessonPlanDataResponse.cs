using TeachPlanner.Api.Contracts.Curriculum;
using TeachPlanner.Api.Contracts.Resources;

namespace TeachPlanner.Api.Contracts.LessonPlans;

public record GetLessonPlanDataResponse(
    List<CurriculumSubjectDto> CurriculumSubjects,
    List<ResourceResponse> Resources);