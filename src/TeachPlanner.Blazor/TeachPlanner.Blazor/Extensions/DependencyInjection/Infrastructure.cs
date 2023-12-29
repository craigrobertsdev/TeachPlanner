using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TeachPlanner.Blazor.Common.Interfaces.Authentication;
using TeachPlanner.Blazor.Common.Interfaces.Curriculum;
using TeachPlanner.Blazor.Common.Interfaces.Persistence;
using TeachPlanner.Blazor.Common.Interfaces.Services;
using TeachPlanner.Blazor.Database;
using TeachPlanner.Blazor.Database.Repositories;
using TeachPlanner.Blazor.Services;
using TeachPlanner.Blazor.Services.Authentication;
using TeachPlanner.Blazor.Services.CurriculumParser;
using TeachPlanner.Blazor.Services.FileStorage;

namespace TeachPlanner.Blazor.Extensions.DependencyInjection;

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

        var serverVersion = ServerVersion.AutoDetect(dbContextSettings.DefaultConnection);

        services.AddDbContext<ApplicationDbContext>(options => options
            //.ReplaceService<IValueConverterSelector, StronglyTypedIdValueConverterSelector>()
            .UseMySql(dbContextSettings.DefaultConnection, serverVersion)
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
        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);

        services.AddSingleton(jwtSettings);
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddAuthentication(x => {
            x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters {
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true
            });

        return services;
    }

    private static IServiceCollection AddCurriculumParser(this IServiceCollection services) {
        services.AddScoped<ICurriculumParser, CurriculumParser>();
        return services;
    }
}