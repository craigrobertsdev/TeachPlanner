using Syncfusion.Licensing;
using TeachPlanner.Api;
using TeachPlanner.Api.Extensions.DependencyInjection;
using TeachPlanner.Shared.Domain.Users;

var builder = WebApplication.CreateBuilder(args);

var syncfusionLicenceKey = builder.Configuration["Syncfusion:LicenseKey"];
SyncfusionLicenseProvider.RegisterLicense(syncfusionLicenceKey);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddApplication();

builder.Services.AddRazorPages();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapIdentityApi<ApplicationUser>();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
    app.UseWebAssemblyDebugging();
}

// enable cors
app.UseCors("wasm");

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseRouting();

app.MapRazorPages();
app.MapFallbackToFile("index.html");

app.MapApi();

app.Run();
