using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Security;
using TeachPlanner.Application.Common.Exceptions;
using TeachPlanner.Application.Common.Interfaces.Persistence;
using TeachPlanner.Domain.Subjects;
using TeachPlanner.Domain.Teachers;
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

    public void SetSubjectsTaughtByTeacher(Teacher teacher, List<Subject> subjects)
    {
        teacher.AddSubjectsTaught(subjects);
    }

    public Task<Teacher?> GetTeacherByEmailAsync(string email, CancellationToken cancellationToken)
    {
        var teacher = _context.Teachers.FirstOrDefaultAsync(t => t.Email == email);
        return teacher;
    }

    public Task<Teacher?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return _context.Teachers.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<Teacher?> GetTeacherByUserId(Guid id)
    {
        var teachers = _context.Teachers
            .Include(t => t.GetStudentsForYear(DateTime.Now.Year))
            .Include(t => t.SummativeAssessments)
            .Include(t => t.FormativeAssessments);

        return await _context.Teachers
            .SingleOrDefaultAsync(t => t.Id == id);
    }

}
