using TeachPlanner.Api.Extensions;
using TeachPlanner.Api.Extensions.DependencyInjection;
using TeachPlanner.Api.Identity;
using TeachPlanner.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

var syncfusionLicenceKey = builder.Configuration["Syncfusion:LicenseKey"];
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(syncfusionLicenceKey);

builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddApplication();

builder.Services.AddAuthorization(x =>
{
    x.AddPolicy(IdentityData.AdminUserPolicyName, policy => policy.RequireClaim(IdentityData.AdminUserClaimName, "true"));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<ErrorHandlingMiddleware>();
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
