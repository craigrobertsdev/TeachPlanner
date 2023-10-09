using TeachPlanner.Api.Common.Interfaces.Curriculum;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Domain.CurriculumSubjects;

namespace TeachPlanner.Api.Services;
/// <summary>
/// This class is responsible for loading the curriculum subjects from the database and storing them in memory.
/// Will be created as a singleton service in the DI container and be the source of truth for all curriculum subjects.
/// </summary>
public sealed class CurriculumService : ICurriculumService
{
    private readonly ISubjectRepository _subjectRepository;
    public List<CurriculumSubject> CurriculumSubjects { get; } = new();

    public CurriculumService(ISubjectRepository subjectRepository)
    {
        _subjectRepository = subjectRepository;
        LoadCurriculumSubjects();
    }

    private async void LoadCurriculumSubjects()
    {
        var subjects = await _subjectRepository.GetCurriculumSubjects(true, CancellationToken.None);

        CurriculumSubjects.AddRange(subjects);
    }
}
