using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
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

    public async void Create(Teacher teacher)
    {
        _context.Teachers.Add(teacher);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Subject>?> GetSubjectsTaughtByTeacher(Guid teacherId, bool? elaborations)
    {
        var teacher = await _context.Teachers
            .Include(t => t.SubjectsTaught)
            .SingleOrDefaultAsync(t => t.Id == teacherId);

        if (teacher == null)
        {
            return null;
        }

        var teacherSubjectIds = teacher.SubjectsTaught.Select(s => s.Id).ToList();

        var subjectQuery = _context.Subjects
            .Where(s => teacherSubjectIds.Contains(s.Id))
            .Where(s => s.Name != "Mathematics")
            .Include(s => s.YearLevels)
            .ThenInclude(yl => yl.Strands)
            .ThenInclude(s => s.Substrands!)
            .ThenInclude(ss => ss.ContentDescriptions);

        var mathsQuery = _context.Subjects
            .Where(s => teacherSubjectIds.Contains(s.Id))
            .Where(s => s.Name == "Mathematics")
            .Include(s => s.YearLevels)
            .ThenInclude(yl => yl.Strands)
            .ThenInclude(s => s.ContentDescriptions!);

        if (elaborations == true)
        {
            subjectQuery.ThenInclude(cd => cd.Elaborations);
            mathsQuery.ThenInclude(cd => cd.Elaborations);
        }

        var subjects = subjectQuery.ToList();
        var maths = mathsQuery.SingleOrDefault();

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

    public async Task<List<Subject>?> SetSubjectsTaughtByTeacher(Guid teacherId, List<string> subjectNames)
    {
        var subjects = _context.Subjects.Where(s => subjectNames.Contains(s.Name)).ToList();

        var teacher = _context.Teachers.FirstOrDefault(t => t.Id == teacherId);
        
        if (teacher == null)
        {
            return null;
        }

        teacher.AddSubjectsTaught(subjects);

        await _context.SaveChangesAsync();

        return subjects;
    }

    public Task<Teacher?> GetTeacherByEmailAsync(string email)
    {
        var teacher = _context.Teachers.FirstOrDefaultAsync(t => t.Email == email);
        return teacher;
    }

    public Task<Teacher?> GetTeacherById(Guid id)
    {
        return _context.Teachers.FirstOrDefaultAsync(t => t.Id == id);
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
