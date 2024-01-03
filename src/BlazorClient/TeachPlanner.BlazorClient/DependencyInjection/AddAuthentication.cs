using Microsoft.AspNetCore.Components.Authorization;
using TeachPlanner.BlazorClient.Authentication;
using TeachPlanner.BlazorClient.Services;

namespace TeachPlanner.BlazorClient.DependencyInjection;

public static class AddAuthentication {
    public static IServiceCollection AddAuthenticationServices(this IServiceCollection services) {
        services.AddScoped<AuthenticationStateManager>();
        services.AddScoped<UserService>();
        services.AddScoped<TeachPlannerAuthenticationStateProvider>();
        services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<TeachPlannerAuthenticationStateProvider>());
        services.AddAuthorizationCore();

        return services;
    }
}
