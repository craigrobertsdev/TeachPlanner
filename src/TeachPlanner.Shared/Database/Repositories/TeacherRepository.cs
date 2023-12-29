using Microsoft.EntityFrameworkCore;
using TeachPlanner.Api.Domain.Teachers.DomainEvents;
using TeachPlanner.Blazor.Common.Interfaces.Persistence;
using TeachPlanner.Blazor.Database;
using TeachPlanner.Shared.Domain.Curriculum;
using TeachPlanner.Shared.Domain.Teachers;
using TeachPlanner.Shared.Domain.Users;
using TeachPlanner.Shared.Domain.YearDataRecords;
using TeachPlanner.Shared.Domain.Curriculum;

namespace TeachPlanner.Blazor.Database.Repositories;

public class TeacherRepository : ITeacherRepository {
    private readonly ApplicationDbContext _context;

    public TeacherRepository(ApplicationDbContext context) {
        _context = context;
    }

    public void Add(Teacher teacher) {
        _context.Teachers.Add(teacher);
    }

    public Task<Teacher?> GetByEmail(string email, CancellationToken cancellationToken) {
        throw new NotImplementedException();
    }

    public async Task<Teacher?> GetById(TeacherId teacherId, CancellationToken cancellationToken) {
        return await _context.Teachers
            .Where(t => t.Id == teacherId)
            .Include(t => t.Resources)
            .Include(t => t.YearDataHistory)
            .Include(t => t.SubjectsTaught)
            .AsSplitQuery()
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Teacher?> GetByUserId(UserId userId, CancellationToken cancellationToken) {
        return await _context.Teachers
            .Where(t => t.UserId == userId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Teacher?> GetWithResources(TeacherId teacherId, CancellationToken cancellationToken) {
        return await _context.Teachers
            .Where(t => t.Id == teacherId)
            .Include(t => t.Resources)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<Resource>> GetResourcesBySubject(TeacherId teacherId, SubjectId subjectId,
        CancellationToken cancellationToken) {
        var teacher = await _context.Teachers
            .Where(t => t.Id == teacherId)
            .Include(t => t.Resources)
            // .Where(r => r.SubjectId == subjectId))
            .FirstOrDefaultAsync(cancellationToken);

        return teacher != null ? teacher.Resources : new List<Resource>();
    }

    public Task<List<CurriculumSubject>> GetSubjectsTaughtByTeacherWithElaborations(TeacherId teacherId,
        CancellationToken cancellationToken) {
        throw new NotImplementedException();
    }

    public Task<List<CurriculumSubject>> GetSubjectsTaughtByTeacherWithoutElaborations(TeacherId teacherId,
        CancellationToken cancellationToken) {
        throw new NotImplementedException();
    }

    public Task<YearData?> GetYearDataByTeacherIdAndYear(TeacherId teacherId, int calendarYear,
        CancellationToken cancellationToken) {
        throw new NotImplementedException();
    }
}