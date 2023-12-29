using TeachPlanner.Shared.Domain.Curriculum;

namespace TeachPlanner.Blazor.Common.Interfaces.Curriculum;

public interface ICurriculumParser {
    List<CurriculumSubject> ParseCurriculum();
}