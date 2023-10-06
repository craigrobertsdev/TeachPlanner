using Microsoft.AspNetCore.Http.Json;
using System.Text.Json.Serialization;
using TeachPlanner.Api;
using TeachPlanner.Api.Extensions.DependencyInjection;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var syncfusionLicenceKey = builder.Configuration["Syncfusion:LicenseKey"];
        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(syncfusionLicenceKey);

        builder.Services
            .AddInfrastructure(builder.Configuration)
            .AddApplication();

        builder.Services.AddAuthorization();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI();

        //app.UseMiddleware<ErrorHandlingMiddleware>();

        // enable cors
        //app.UseCors(builder => builder
        //    .AllowAnyOrigin()
        //    .AllowAnyMethod()
        //    .AllowAnyHeader());

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapApi();

        app.Run();
    }
}