using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TeachPlanner.Application.Common.Interfaces.Authentication;
using TeachPlanner.Application.Common.Interfaces.Curriculum;
using TeachPlanner.Application.Common.Interfaces.Persistence;
using TeachPlanner.Infrastructure.Authentication;
using TeachPlanner.Infrastructure.Curriculum;
using TeachPlanner.Infrastructure.Persistence;
using TeachPlanner.Infrastructure.Persistence.DbContexts;
using TeachPlanner.Infrastructure.Persistence.Repositories;

namespace TeachPlanner.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);
        services.AddAuth(configuration);
        services.AddCurriculum();
        return services;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var dbContextSettings = new DbContextSettings();
        configuration.Bind(DbContextSettings.SectionName, dbContextSettings);

        var serverVersion = ServerVersion.AutoDetect(dbContextSettings.DefaultConnection);

        services.AddDbContext<ApplicationDbContext>(options => options
            .UseMySql(dbContextSettings.DefaultConnection, serverVersion)
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors());


        services.AddScoped<ILessonRepository, LessonPlanRepository>();
        services.AddScoped<ITeacherRepository, TeacherRepository>();
        services.AddScoped<ICurriculumRepository, CurriculumRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    private static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);

        services.AddSingleton(jwtSettings);
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidateAudience = true,
                ValidAudience = jwtSettings.Audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                ValidateLifetime = true,
            }
            );

        return services;
    }

    private static IServiceCollection AddCurriculum(this IServiceCollection services)
    {
        services.AddScoped<ICurriculumParser, CurriculumParser>();
        return services;
    }
}
