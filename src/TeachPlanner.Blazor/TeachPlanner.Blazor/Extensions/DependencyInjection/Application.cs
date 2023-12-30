using System.Reflection;
using FluentValidation;
using TeachPlanner.Shared.Database;

namespace TeachPlanner.Blazor.Extensions.DependencyInjection;

public static class Application {
    public static IServiceCollection AddApplication(this IServiceCollection services) {
        services.AddMediatR(config => { 
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            config.RegisterServicesFromAssemblyContaining<ApplicationDbContext>();
            });

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}