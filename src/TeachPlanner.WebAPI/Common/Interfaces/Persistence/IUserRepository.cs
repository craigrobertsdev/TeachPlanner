using TeachPlanner.Api.Domain.Users;

namespace TeachPlanner.Api.Common.Interfaces.Persistence;

public interface IUserRepository
{
    public Task<User?> GetByEmail(string email, CancellationToken cancellationToken);
}
