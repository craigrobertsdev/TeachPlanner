using System.Reflection;
using FluentValidation;

namespace TeachPlanner.Api.Extensions.DependencyInjection;

public static class Application {
    public static IServiceCollection AddApplication(this IServiceCollection services) {
        services.AddMediatR(config => { config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()); });

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}