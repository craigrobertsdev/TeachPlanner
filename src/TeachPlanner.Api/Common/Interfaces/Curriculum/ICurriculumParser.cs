using TeachPlanner.Api.Domain.CurriculumSubjects;

namespace TeachPlanner.Api.Common.Interfaces.Curriculum;

public interface ICurriculumParser {
    List<CurriculumSubject> ParseCurriculum();
}