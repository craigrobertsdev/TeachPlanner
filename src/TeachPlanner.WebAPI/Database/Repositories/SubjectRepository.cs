using Microsoft.EntityFrameworkCore;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Entities.Subjects;

namespace TeachPlanner.Api.Database.Repositories;
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

    public Task<List<Subject>> GetSubjectsById(List<SubjectId> subjectIds, CancellationToken cancellationToken)
    {
        return _context.Subjects.Where(s => subjectIds.Contains(s.Id)).ToListAsync(cancellationToken);
    }
}
