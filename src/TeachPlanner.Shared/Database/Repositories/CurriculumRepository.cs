using System.CodeDom;
using Microsoft.EntityFrameworkCore;
using TeachPlanner.Blazor.Common.Interfaces.Persistence;
using TeachPlanner.Blazor.Database;
using TeachPlanner.Shared.Enums;
using TeachPlanner.Shared.Domain.Curriculum;
using TeachPlanner.Shared.Domain.Curriculum;

namespace TeachPlanner.Blazor.Database.Repositories;

public class CurriculumRepository : ICurriculumRepository {
    private readonly ApplicationDbContext _context;

    public CurriculumRepository(ApplicationDbContext context) {
        _context = context;
    }

    public async Task AddCurriculum(List<CurriculumSubject> subjects, CancellationToken cancellationToken) {
        // clear existing curriculum subjects
        var curriculumSubjects = await _context.CurriculumSubjects
            .ToListAsync(cancellationToken);

        _context.CurriculumSubjects.RemoveRange(curriculumSubjects);
        await _context.SaveChangesAsync(cancellationToken);

        // add new subjects
        foreach (var subject in subjects) _context.CurriculumSubjects.Add(subject);
    }

    public async Task<List<CurriculumSubject>> GetAllSubjects(CancellationToken cancellationToken) {
        return await _context.CurriculumSubjects.ToListAsync(cancellationToken);
    }

    public async Task<List<CurriculumSubject>> GetSubjectsById(List<SubjectId> subjectIds, CancellationToken cancellationToken) {
        return await _context.CurriculumSubjects
            .Where(s => subjectIds.Contains(s.Id))
            .ToListAsync(cancellationToken);
    }

    public async Task<List<CurriculumSubject>> GetSubjectsByName(List<string> subjectNames, CancellationToken cancellationToken) {
        return await _context.CurriculumSubjects
            .Where(s => subjectNames.Contains(s.Name))
            .ToListAsync(cancellationToken);
    }

    public async Task<List<CurriculumSubject>> GetSubjectsByYearLevels(List<YearLevelValue> yearLevels, CancellationToken cancellationToken) {
        {
            var subjects = await _context.CurriculumSubjects
                .Include(s => s.YearLevels)
                .ThenInclude(yl => yl.Strands)
                .ThenInclude(s => s.ContentDescriptions)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return subjects.Select(s => CurriculumSubject.Create(s.Name, s.RemoveYearLevelsNotTaught(yearLevels))).ToList();
        }
    }
}