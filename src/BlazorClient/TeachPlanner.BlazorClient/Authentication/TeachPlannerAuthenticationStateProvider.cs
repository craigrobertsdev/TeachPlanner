using Microsoft.AspNetCore.Components.Authorization;
using TeachPlanner.BlazorClient.Services;

namespace TeachPlanner.BlazorClient.Authentication;

public class TeachPlannerAuthenticationStateProvider : AuthenticationStateProvider {
    private readonly UserService _userService;

    public TeachPlannerAuthenticationStateProvider(UserService userService) {
        _userService = userService;
    }
    public override Task<AuthenticationState> GetAuthenticationStateAsync() {
        throw new NotImplementedException();
    }
}
