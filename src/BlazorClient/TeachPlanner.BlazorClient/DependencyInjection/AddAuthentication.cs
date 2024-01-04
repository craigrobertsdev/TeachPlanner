using Microsoft.AspNetCore.Components.Authorization;
using TeachPlanner.BlazorClient.Services;

namespace TeachPlanner.BlazorClient.DependencyInjection;

public static class AddAuthentication {
    public static IServiceCollection AddAuthenticationServices(this IServiceCollection services) {
        services.AddSingleton<AuthenticationStateProvider>();
        services.AddScoped<UserService>();
        services.AddApiAuthorization();
        services.AddAuthorizationCore();

        return services;
    }
}
