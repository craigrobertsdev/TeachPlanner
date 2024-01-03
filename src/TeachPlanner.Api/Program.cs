using Microsoft.OpenApi.Models;
using Syncfusion.Licensing;
using TeachPlanner.Api;
using TeachPlanner.Api.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

var syncfusionLicenceKey = builder.Configuration["Syncfusion:LicenseKey"];
SyncfusionLicenseProvider.RegisterLicense(syncfusionLicenceKey);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddApplication();

builder.Services.AddAuthorization();

builder.Services.AddRazorPages();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt => {
    opt.SwaggerDoc("v1", new OpenApiInfo {
        Title = "TeachPlanner Api",
        Version = "v1"
    });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
builder.Services.AddCors(opts => {
    opts.AddDefaultPolicy(builder => {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
    app.UseWebAssemblyDebugging();
}


// enable cors
app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapFallbackToFile("index.html");

app.MapApi();

app.Run();
