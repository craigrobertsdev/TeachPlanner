using Application.Common.Interfaces.Persistence;
using Domain.TeacherAggregate;
using Domain.UserAggregate;
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

    public async Task<Teacher?> GetTeacherByUserId(UserId userId)
    {
        return await _context.Teachers
            .SingleOrDefaultAsync(t => t.UserId == userId);
    }
}
