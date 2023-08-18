using Application.Common.Interfaces.Persistence;
using Domain.TeacherAggregate;
using Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

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

    public Task<Teacher?> GetTeacherByEmail(string email)
    {
        return _context.Teachers.FirstOrDefaultAsync(t => t.Email == email);
    }

    public Task<Teacher?> GetTeacherById(Guid id)
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
