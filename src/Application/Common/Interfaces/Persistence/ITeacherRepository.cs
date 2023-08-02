using Domain.TeacherAggregate;
using Domain.UserAggregate;

namespace Application.Common.Interfaces.Persistence;
public interface ITeacherRepository
{
    void Create(Teacher teacher);

    Task<Teacher?> GetTeacherByUserId(Guid userId);
}
