using Domain.TeacherAggregate;

namespace Application.Common.Interfaces.Persistence;
public interface ITeacherRepository
{
    void Create(Teacher teacher);
    Task<Teacher?> GetTeacherByEmail(string email);
    Task<Teacher?> GetTeacherById(Guid userId);
}
