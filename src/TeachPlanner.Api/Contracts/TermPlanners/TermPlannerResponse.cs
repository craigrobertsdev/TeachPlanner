using TeachPlanner.Api.Contracts.Subjects;
using TeachPlanner.Api.Domain.Common.Enums;
using TeachPlanner.Api.Domain.TermPlanners;

namespace TeachPlanner.Api.Contracts.TermPlanners;

public record TermPlannerResponse(
    List<TermPlanResponse> TermPlans,
    List<YearLevelValue> YearLevels,
    int CalendarYear) {
    public static TermPlannerResponse Create(TermPlanner termPlanner) {
        return new TermPlannerResponse(
            CreateTermPlanResponses(termPlanner.TermPlans),
            termPlanner.YearLevels.ToList(),
            termPlanner.CalendarYear);
    }

    private static List<TermPlanResponse> CreateTermPlanResponses(IEnumerable<TermPlan> termPlans) {
        var termPlanResponses = termPlans.Select(tp => new TermPlanResponse(
            SubjectResponse.CreateCurriculumSubjectResponses(tp.Subjects, false),
            tp.TermNumber)).ToList();

        return termPlanResponses;
    }
}

public record TermPlanResponse(
    List<SubjectResponse> Subjects,
    int TermNumber);