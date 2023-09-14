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

    public async void Create(Teacher teacher, CancellationToken cancellationToken)
    {
        _context.Teachers.Add(teacher);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Subject>> GetSubjectsTaughtByTeacherWithoutElaborations(Guid teacherId, CancellationToken cancellationToken)
    {
        var subjectQuery = _context.Teachers
            .Where(t => t.Id == teacherId)
            .SelectMany(t => t.SubjectsTaught)
            .AsNoTracking()
            .Where(s => s.Name != "Mathematics")
            .Include(s => s.YearLevels)
            .ThenInclude(yl => yl.Strands)
            .ThenInclude(s => s.Substrands!)
            .ThenInclude(s => s.ContentDescriptions);

        var mathsQuery = _context.Teachers
            .Where(t => t.Id == teacherId)
            .SelectMany(t => t.SubjectsTaught)
            .AsNoTracking()
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
        var subjectQuery = _context.Teachers
            .Where(t => t.Id == teacherId)
            .SelectMany(t => t.SubjectsTaught)
            .AsNoTracking()
            .Where(s => s.Name != "Mathematics")
            .Include(s => s.YearLevels)
            .ThenInclude(yl => yl.Strands)
            .ThenInclude(s => s.Substrands!)
            .ThenInclude(s => s.ContentDescriptions)
            .ThenInclude(cd => cd.Elaborations);

        var mathsQuery = _context.Teachers
            .Where(t => t.Id == teacherId)
            .SelectMany(t => t.SubjectsTaught)
            .AsNoTracking()
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

    public async Task<List<Subject>> SetSubjectsTaughtByTeacher(Guid teacherId, List<string> subjectNames, CancellationToken cancellationToken)
    {
        var subjects = _context.Subjects.Where(s => subjectNames.Contains(s.Name)).ToList();

        var teacher = _context.Teachers.FirstOrDefault(t => t.Id == teacherId);

        if (teacher == null)
        {
            throw new TeacherNotFoundException();
        }

        teacher.AddSubjectsTaught(subjects);

        await _context.SaveChangesAsync();

        return subjects;
    }

    public Task<Teacher?> GetTeacherByEmailAsync(string email, CancellationToken cancellationToken)
    {
        var teacher = _context.Teachers.FirstOrDefaultAsync(t => t.Email == email);
        return teacher;
    }

    public Task<Teacher?> GetTeacherById(Guid id, CancellationToken cancellationToken)
    {
        return _context.Teachers.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<Teacher?> GetTeacherByUserId(Guid id)
    {
        var teachers = _context.Teachers
            .Include(t => t.Students)
            .Include(t => t.SummativeAssessments)
            .Include(t => t.FormativeAssessments);

        return await _context.Teachers
            .SingleOrDefaultAsync(t => t.Id == id);
    }

}
