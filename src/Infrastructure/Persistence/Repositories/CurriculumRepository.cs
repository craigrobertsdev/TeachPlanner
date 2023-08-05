using Application.Common.Interfaces.Persistence;
using Domain.SubjectAggregates;
using Infrastructure.Persistence.DbContexts;

namespace Infrastructure.Persistence.Repositories;
public class CurriculumRepository : ICurriculumRepository
{
    private readonly ApplicationDbContext _context;

    public CurriculumRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task SaveCurriculum(List<Subject> subjects)
    {
        foreach (var subject in subjects)
        {
            _context.Subjects.Add(subject);
        }

        return _context.SaveChangesAsync();
    }
}
