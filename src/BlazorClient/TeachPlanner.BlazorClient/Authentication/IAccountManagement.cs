using TeachPlanner.BlazorClient.Authentication.Models;

namespace TeachPlanner.BlazorClient.Authentication;

public interface IAccountManagement {
    public Task<FormResult> LoginAsync(string email, string password);
    public Task LogoutAsync();
    public Task<FormResult> RegisterAsync(string email, string password, string confirmPassword);
    public Task<bool> CheckAuthenticatedAsync();
}
