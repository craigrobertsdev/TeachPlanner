using TeachPlanner.Api.Common.Interfaces.Curriculum;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Domain.CurriculumSubjects;

namespace TeachPlanner.Api.Services;

/// <summary>
///     This class is responsible for loading the curriculum subjects from the database and storing them in memory.
///     Will be created as a singleton service in the DI container and be the source of truth for all curriculum subjects.
/// </summary>
public sealed class CurriculumService : ICurriculumService
{
    private readonly IServiceProvider _serviceProvider;

    public CurriculumService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        LoadCurriculumSubjects();
    }

    public List<CurriculumSubject> CurriculumSubjects { get; } = new();

    private async void LoadCurriculumSubjects()
    {
        var subjectRepository = _serviceProvider.GetRequiredService<ISubjectRepository>();
        var subjects = await subjectRepository.GetCurriculumSubjects(true, CancellationToken.None);

        CurriculumSubjects.AddRange(subjects);
    }
}