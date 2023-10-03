using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TeachPlanner.Api.Common.Interfaces.Authentication;
using TeachPlanner.Api.Common.Interfaces.Curriculum;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Database;
using TeachPlanner.Api.Database.Converters;
using TeachPlanner.Api.Services.Authentication;
using TeachPlanner.Api.Services.CurriculumParser;

namespace TeachPlanner.Api.Extensions.DependencyInjection;

public static class Infrastructure
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
            .ReplaceService<IValueConverterSelector, StronglyTypedIdValueConverterSelector>()
            .UseMySql(dbContextSettings.DefaultConnection, serverVersion)
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors());


        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    private static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {

        //services.AddIdentity<IdentityUser, IdentityRole>(options =>
        //{
        //    options.Password.RequireDigit = true;
        //    options.Password.RequireUppercase = true;
        //    options.Password.RequireNonAlphanumeric = true;
        //    options.Password.RequiredLength = 8;
        //})
        //    .AddDefaultTokenProviders();

        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);

        services.AddSingleton(jwtSettings);
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddAuthentication(x =>
        {
            x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
            });

        return services;
    }

    private static IServiceCollection AddCurriculum(this IServiceCollection services)
    {
        services.AddScoped<ICurriculumParser, CurriculumParser>();
        return services;
    }
}
