using TeachPlanner.Shared.Contracts.Authentication;
using TeachPlanner.Shared.Contracts.Teachers;

namespace TeachPlanner.BlazorClient.Services;

public interface IAuthenticationService {
    ValueTask<string> GetJWT();
    Task<TeacherModel> Login(LoginModel model);
    Task Logout();

}
