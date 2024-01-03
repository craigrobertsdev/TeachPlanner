using TeachPlanner.Shared.Common.Interfaces.Services;
using TeachPlanner.Shared.Contracts.Curriculum;

namespace TeachPlanner.Api.Features.Curriculum;

public static class GetCurriculumSubjectNames {
    public class Handler {
        private readonly ICurriculumService _curriculumService;

        public Handler(ICurriculumService curriculumService) {
            _curriculumService = curriculumService;
        }

        public CurriculumSubjectsNamesResponse Handle() {
            var curriculum = _curriculumService.GetSubjectNames();
            return new CurriculumSubjectsNamesResponse(curriculum ?? new List<string>());
        }
    }

    public static IResult Delegate(ICurriculumService curriculumService) {
        var handler = new Handler(curriculumService);
        var result = handler.Handle();
        return Results.Ok(result);
    }
}
