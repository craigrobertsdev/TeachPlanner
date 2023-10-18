using TeachPlanner.Api.Domain.CurriculumSubjects;

namespace TeachPlanner.Api.Common.Interfaces.Curriculum;

public interface ICurriculumService
{
    List<CurriculumSubject> CurriculumSubjects { get; }
}