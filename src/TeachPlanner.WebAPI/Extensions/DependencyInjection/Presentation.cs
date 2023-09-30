using TeachPlanner.Api.Common.Mappings;

namespace TeachPlanner.Api.Extensions.DependencyInjection;

public static class Presentation
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddMappings();
        services.AddControllers();

        return services;
    }
}
