using Domain.TeacherAggregate;

namespace Application.Common.Interfaces.Persistence;
public interface ITeacherRepository
{
    void Create(Teacher teacher);
    Task<Teacher?> GetTeacherByEmailAsync(string email);
    Task<Teacher?> GetTeacherByIdAsync(Guid userId);
}
