using TeachPlanner.Shared.Domain.Curriculum;

namespace TeachPlanner.Shared.Common.Interfaces.Services;

public interface ICurriculumService {
    List<CurriculumSubject> CurriculumSubjects { get; }
    List<string> GetSubjectNames();
}