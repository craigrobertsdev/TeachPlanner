using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using TeachPlanner.Api.Common.Interfaces.Curriculum;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Database;
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
        CurriculumSubjects = Task.Run(LoadCurriculumSubjects).Result;
    }

    public List<CurriculumSubject> CurriculumSubjects { get; } = new();

    private async Task<List<CurriculumSubject>> LoadCurriculumSubjects()
    {
        using var scope = _serviceProvider.CreateScope();
        var _context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var subjects = await _context.CurriculumSubjects.ToListAsync();
        return subjects;

    }

    public List<string> GetSubjectNames()
    {
        return CurriculumSubjects.Select(x => x.Name).ToList();
    }
}