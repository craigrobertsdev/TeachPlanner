using Domain.TeacherAggregate;
using Domain.UserAggregate.ValueObjects;

namespace Application.Common.Interfaces.Persistence;
public interface ITeacherRepository
{
    void Create(Teacher teacher);

    Task<Teacher?> GetTeacherByUserId(UserIdForReference userId);
}
