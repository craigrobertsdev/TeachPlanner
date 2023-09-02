using Microsoft.EntityFrameworkCore;
using TeachPlanner.Application.Common.Interfaces.Persistence;
using TeachPlanner.Domain.Teacher;
using TeachPlanner.Infrastructure.Persistence.DbContexts;
using TeachPlanner.Application.Common.Errors;

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

    public async Task<List<Guid>?> GetSubjectsTaughtByTeacher(Guid teacherId)
    {
        var teacher = await _context.Teachers
            .Include(t => t.SubjectIds)
            .SingleOrDefaultAsync(t => t.Id == teacherId);

        if (teacher == null)
        {
            return null;
        }

        var subjectIds = _context.Subjects
            .Where(s => teacher.SubjectIds.Contains(s.Id))
            .Select(s => s.Id);

        return subjectIds.ToList();
    }

    public Task<Teacher?> GetTeacherByEmailAsync(string email)
    {
        var teacher = _context.Teachers.FirstOrDefaultAsync(t => t.Email == email);
        return teacher;
    }

    public Task<Teacher?> GetTeacherByIdAsync(Guid id)
    {
        return _context.Teachers.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<Teacher?> GetTeacherByUserId(Guid id)
    {
        var teachers = _context.Teachers
            .Include(t => t.StudentIds)
            .Include(t => t.SummativeAssessmentIds)
            .Include(t => t.FormativeAssessmentIds);

        return await _context.Teachers
            .SingleOrDefaultAsync(t => t.Id == id);
    }
}
