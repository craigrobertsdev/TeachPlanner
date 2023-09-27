using Microsoft.EntityFrameworkCore;
using TeachPlanner.Application.Common.Exceptions;
using TeachPlanner.Application.Common.Interfaces.Persistence;
using TeachPlanner.Domain.Subjects;
using TeachPlanner.Domain.Teachers;
using TeachPlanner.Domain.YearDataRecords;
using TeachPlanner.Infrastructure.Persistence.DbContexts;

namespace TeachPlanner.Infrastructure.Persistence.Repositories;

public class TeacherRepository : ITeacherRepository
{
    private readonly ApplicationDbContext _context;

    public TeacherRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Create(Teacher teacher)
    {
        _context.Teachers.Add(teacher);
    }

    public async Task<List<Subject>> GetSubjectsTaughtByTeacherWithoutElaborations(Guid teacherId, CancellationToken cancellationToken)
    {
        var subjectIds = _context.Teachers
            .Where(t => t.Id == teacherId)
            .SelectMany(t => t.SubjectsTaughtIds)
            .ToList();

        var subjectQuery = _context.Subjects
            .AsNoTracking()
            .Where(s => subjectIds.Contains(s.Id))
            .Where(s => s.Name != "Mathematics")
            .Include(s => s.YearLevels)
            .ThenInclude(yl => yl.Strands)
            .ThenInclude(s => s.Substrands!)
            .ThenInclude(s => s.ContentDescriptions);

        var mathsQuery = _context.Subjects
            .AsNoTracking()
            .Where(s => subjectIds.Contains(s.Id))
            .Where(s => s.Name == "Mathematics")
            .Include(s => s.YearLevels)
            .ThenInclude(yl => yl.Strands)
            .ThenInclude(s => s.ContentDescriptions!);


        var subjects = await subjectQuery.ToListAsync();
        var maths = await mathsQuery.SingleOrDefaultAsync();

        if (maths != null)
        {
            subjects.Add(maths);
        }

        if (subjects.Count == 0)
        {
            return new List<Subject>();
        }

        return subjects;
    }

    public async Task<List<Subject>> GetSubjectsTaughtByTeacherWithElaborations(Guid teacherId, CancellationToken cancellationToken)
    {
        var subjectIds = _context.Teachers
            .Where(t => t.Id == teacherId)
            .SelectMany(t => t.SubjectsTaughtIds)
            .ToList();

        var subjectQuery = _context.Subjects
            .AsNoTracking()
            .Where(s => subjectIds.Contains(s.Id))
            .Where(s => s.Name != "Mathematics")
            .Include(s => s.YearLevels)
            .ThenInclude(yl => yl.Strands)
            .ThenInclude(s => s.Substrands!)
            .ThenInclude(s => s.ContentDescriptions)
            .ThenInclude(cd => cd.Elaborations);

        var mathsQuery = _context.Subjects
            .AsNoTracking()
            .Where(s => subjectIds.Contains(s.Id))
            .Where(s => s.Name == "Mathematics")
            .Include(s => s.YearLevels)
            .ThenInclude(yl => yl.Strands)
            .ThenInclude(s => s.ContentDescriptions!)
            .ThenInclude(cd => cd.Elaborations);


        var subjects = await subjectQuery.ToListAsync();
        var maths = await mathsQuery.SingleOrDefaultAsync();

        if (maths != null)
        {
            subjects.Add(maths);
        }

        if (subjects.Count == 0)
        {
            return new List<Subject>();
        }

        return subjects;
    }

    public async Task<Teacher?> GetByEmail(string email, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Where(u => u.Email == email)
            .FirstOrDefaultAsync(cancellationToken);

        if (user == null)
        {
            return null;
        }

        return await _context.Teachers
            .Where(t => t.UserId == Guid.Parse(user.Id))
            .Include(t => t.SummativeAssessments)
            .Include(t => t.FormativeAssessments)
            .Include(t => t.YearDataHistory.Where(yd => yd.Key == DateTime.Now.Year))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public Task<Teacher?> GetByUserId(Guid userId, CancellationToken cancellationToken)
    {
        return _context.Teachers
            .Where(t => t.UserId == userId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Teacher?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Teachers
            .Include(t => t.YearDataHistory)
            .Include(t => t.SummativeAssessments)
            .Include(t => t.FormativeAssessments)
            .SingleOrDefaultAsync(t => t.Id == id, cancellationToken);
    }
    public async Task<YearData?> GetYearDataByTeacherIdAndYear(Guid teacherId, int calendarYear, CancellationToken cancellationToken)
    {
        //var teacher = await _context.Teachers
        //    .Include(t => t.YearDataHistory)
        //    .FirstOrDefaultAsync(t => t.Id == teacherId, cancellationToken);

        //if (teacher is null)
        //{
        //    throw new TeacherNotFoundException();
        //}

        //return teacher.GetYearData(calendarYear);
        throw new NotImplementedException();

    }
}
