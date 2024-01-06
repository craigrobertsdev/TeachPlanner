using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace TeachPlanner.BlazorClient.Authentication;

public class TeachPlannerAuthenticationStateProvider : AuthenticationStateProvider {
    public async override Task<AuthenticationState> GetAuthenticationStateAsync() {
        var anonymous = new ClaimsIdentity();
        return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(anonymous)));    
    }
}
