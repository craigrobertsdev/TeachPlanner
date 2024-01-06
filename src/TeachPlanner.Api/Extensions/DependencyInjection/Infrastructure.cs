using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TeachPlanner.Api.Services;
using TeachPlanner.Api.Services.Authentication;
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
        // add identity and opt-in to endpoints
        services.AddIdentityCore<ApplicationUser>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);

        services.AddSingleton(jwtSettings);
        
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