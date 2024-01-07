using TeachPlanner.Shared.Contracts.Authentication;

namespace TeachPlanner.BlazorClient.Services;

public interface IAuthenticationService {
    event Action<string?>? LoginChange;

    ValueTask<string> GetJwt();
    Task<DateTime> Login(LoginModel model);
    Task Logout();
    Task<bool> Refresh();
}
