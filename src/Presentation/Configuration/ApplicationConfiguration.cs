using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure;

namespace Presentation.Configuration;

public static class ApplicationConfiguration {
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration) {
        DependencyInjection.ConfigureDataService(services, configuration);

        return services;
    }
}
