using TeachPlanner.Api.Controllers;
using TeachPlanner.Api.Extensions;
using TeachPlanner.Api.Identity;
using TeachPlanner.Api.Middleware;
using TeachPlanner.Application;
using TeachPlanner.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var syncfusionLicenceKey = builder.Configuration["Syncfusion:LicenseKey"];
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(syncfusionLicenceKey);

builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddPresentation()
    .AddApplication();

builder.Services.AddAuthorization(x =>
{
    x.AddPolicy(IdentityData.AdminUserPolicyName, policy => policy.RequireClaim(IdentityData.AdminUserClaimName, "true"));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddControllers(options =>
//{
//    options.Filters.Add<MyActionFilter>();
//});

var app = builder.Build();

// Configure the HTTP request pipeline.

//app.UseExceptionHandler("/error");
app.UseMiddleware<ErrorHandlingMiddleware>();

// enable cors
app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
