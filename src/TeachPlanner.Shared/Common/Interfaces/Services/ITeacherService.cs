using TeachPlanner.Shared.Domain.Teachers;

namespace TeachPlanner.Shared.Common.Interfaces.Services;
public interface ITeacherService {
    Task<Guid?> GetTeacherId(string userId, CancellationToken cancellationToken);
}
