using Microsoft.EntityFrameworkCore;
using TeachPlanner.Application.Common.Interfaces.Persistence;
using TeachPlanner.Domain.Subjects;
using TeachPlanner.Infrastructure.Persistence.DbContexts;

namespace TeachPlanner.Infrastructure.Persistence.Repositories;
public class SubjectRepository : ISubjectRepository
{
    private readonly ApplicationDbContext _context;

    public SubjectRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<List<Subject>> GetCurriculum(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<List<Subject>> GetSubjectsById(List<Guid> subjectIds, CancellationToken cancellationToken) 
    { 
        return _context.Subjects.Where(s => subjectIds.Contains(s.Id)).ToListAsync(cancellationToken);
    }
}
