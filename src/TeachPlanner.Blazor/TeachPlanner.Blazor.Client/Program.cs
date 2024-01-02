using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TeachPlanner.Blazor.Client;
using TeachPlanner.Blazor.Client.Services;
using TeachPlanner.Shared.Common.Interfaces.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddAuthorizationCore();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();
builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddSingleton<ApplicationState>();

await builder.Build().RunAsync();
