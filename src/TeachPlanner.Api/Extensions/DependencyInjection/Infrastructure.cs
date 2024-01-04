using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TeachPlanner.Api.Services;
using TeachPlanner.Api.Services.CurriculumParser;
using TeachPlanner.Api.Services.FileStorage;
using TeachPlanner.Shared.Common.Interfaces.Curriculum;
using TeachPlanner.Shared.Common.Interfaces.Persistence;
using TeachPlanner.Shared.Common.Interfaces.Services;
using TeachPlanner.Shared.Database;
using TeachPlanner.Shared.Database.Repositories;
using TeachPlanner.Shared.Domain.Users;

namespace TeachPlanner.Api.Extensions.DependencyInjection;

public static class Infrastructure {
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) {
        services.AddPersistence(configuration);
        services.AddServices();
        services.AddAuth(configuration);
        services.AddCurriculumParser();
        return services;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration) {
        var dbContextSettings = new DbContextSettings();
        configuration.Bind(DbContextSettings.SectionName, dbContextSettings);

        services.AddDbContext<ApplicationDbContext>(options => options
            .UseSqlServer(dbContextSettings.DefaultConnection)
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors());


        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAssessmentRepository, AssessmentRepository>();
        services.AddScoped<ICurriculumRepository, CurriculumRepository>();
        services.AddScoped<ILessonPlanRepository, LessonPlanRepository>();
        services.AddScoped<IPlannerTemplateRepository, PlannerTemplateRepository>();
        services.AddScoped<ISubjectRepository, SubjectRepository>();
        services.AddScoped<ITeacherRepository, TeacherRepository>();
        services.AddScoped<ITermPlannerRepository, TermPlannerRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IWeekPlannerRepository, WeekPlannerRepository>();
        services.AddScoped<IYearDataRepository, YearDataRepository>();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services) {
        services.AddSingleton<ICurriculumService, CurriculumService>();
        services.AddSingleton<ITermDatesService, TermDatesService>();
        services.AddTransient<IStorageManager, StorageManager>();

        return services;
    }

    private static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration) {
        // cookie authentication
        services.AddAuthentication(IdentityConstants.ApplicationScheme).AddIdentityCookies();

        // configure authorisation
        services.AddAuthorizationBuilder();

        // add identity and opt-in to endpoints
        services.AddIdentityCore<ApplicationUser>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddApiEndpoints();

        // add CORS policy for WASM client
        services.AddCors(
            options => options.AddPolicy(
                "wasm",
                policy => policy.WithOrigins(configuration["ServerUrl"] ?? "https://localhost:5000",
                configuration["ClientUrl"] ?? "https://localhost:5001")
                .AllowAnyMethod()
                .SetIsOriginAllowed(pol => true)
                .AllowAnyHeader()
                .AllowCredentials()));

        return services;
    }

    private static IServiceCollection AddCurriculumParser(this IServiceCollection services) {
        services.AddScoped<ICurriculumParser, CurriculumParser>();
        return services;
    }
}