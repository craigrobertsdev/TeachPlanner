using TeachPlanner.Shared.Domain.Curriculum;

namespace TeachPlanner.Shared.Common.Interfaces.Curriculum;

public interface ICurriculumParser {
    List<CurriculumSubject> ParseCurriculum();
}