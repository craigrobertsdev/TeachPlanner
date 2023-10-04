using Microsoft.EntityFrameworkCore;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Domain.Subjects;
using TeachPlanner.Api.Domain.Teachers;
using TeachPlanner.Api.Domain.YearDataRecords;

namespace TeachPlanner.Api.Database.Repositories;

public class TeacherRepository : ITeacherRepository
{
    private readonly ApplicationDbContext _context;

    public TeacherRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public void Create(Teacher teacher)
    {
        throw new NotImplementedException();
    }

    public Task<Teacher?> GetByEmail(string email, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Teacher?> GetById(TeacherId teacherId, CancellationToken cancellationToken)
    {
        return await _context.Teachers
                .Where(t => t.Id == teacherId)
                .Include(t => t.Resources)
                .AsSplitQuery()
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
    }

    public Task<Teacher?> GetByUserId(Guid userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<List<Subject>> GetSubjectsTaughtByTeacherWithElaborations(TeacherId teacherId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<List<Subject>> GetSubjectsTaughtByTeacherWithoutElaborations(TeacherId teacherId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<YearData?> GetYearDataByTeacherIdAndYear(TeacherId teacherId, int calendarYear, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
