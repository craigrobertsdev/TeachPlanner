using TeachPlanner.Shared.Domain.Curriculum;

namespace TeachPlanner.Blazor.Common.Interfaces.Services;

public interface ICurriculumService {
    List<CurriculumSubject> CurriculumSubjects { get; }
    List<string> GetSubjectNames();
}