using TeachPlanner.Api.Domain.CurriculumSubjects;

namespace TeachPlanner.Api.Common.Interfaces.Services;

public interface ICurriculumService
{
    List<CurriculumSubject> CurriculumSubjects { get; }
    List<string> GetSubjectNames();
}