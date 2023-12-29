using TeachPlanner.Shared.Domain.Users;

namespace TeachPlanner.Blazor.Common.Interfaces.Persistence;

public interface IUserRepository {
    public Task<User?> GetByEmail(string email, CancellationToken cancellationToken);
    public void Add(User user);
}