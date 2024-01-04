using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using TeachPlanner.BlazorClient.Authentication.Models;

namespace TeachPlanner.BlazorClient.Authentication;

public class CookieAuthenticationStateProvider : AuthenticationStateProvider, IAccountManagement {
    // Map the JavaScript formatted properties to C# classes
    private readonly JsonSerializerOptions _jsonSerializerOptions = new() {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };

    private readonly HttpClient _httpClient;

    private bool _isAuthenticated;

    // Default principal for unauthenticated users
    private readonly ClaimsPrincipal Unauthenticated = new(new ClaimsIdentity());

    public async Task<FormResult> RegisterAsync(string email, string password, string confirmPassword) {
        string[] defaultDetail = ["An unknown error has occurred."];

        try {
            var result = await _httpClient.PostAsJsonAsync(
                "register",
                new {
                    email,
                    password,
                    confirmPassword
                });

            if (result.IsSuccessStatusCode) {
                return new FormResult { Succeeded = true };
            }

            var details = await result.Content.ReadAsStringAsync();
            var problemDetails = JsonDocument.Parse(details);
            var errors = new List<string>();
            var errorList = problemDetails.RootElement.GetProperty("errors");

            foreach (var errorEntry in errorList.EnumerateObject()) {
                if (errorEntry.Value.ValueKind == JsonValueKind.String) {
                    errors.Add(errorEntry.Value.GetString()!);
                } else if (errorEntry.Value.ValueKind == JsonValueKind.Array) {
                    errors.AddRange(errorEntry.Value.EnumerateArray()
                        .Select(e => e.GetString() ?? string.Empty)
                        .Where(e => !string.IsNullOrEmpty(e)));
                }
            }
            return new FormResult {
                Succeeded = false,
                ErrorList = problemDetails == null ? defaultDetail : [.. errors]
            };
        } catch { }

        return new FormResult {
            Succeeded = false,
            ErrorList = defaultDetail
        };
    }

    public async Task<FormResult> LoginAsync(string email, string password) {
        try {
            var result = await _httpClient.PostAsJsonAsync(
       "login",
                  new {
                      email,
                      password
                  });

            if (result.IsSuccessStatusCode) {
                NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
                return new FormResult { Succeeded = true }; 
            }
        } catch { }

        return new FormResult {
            Succeeded = false,
            ErrorList = new[] { "Invalid username or password." }
        };
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync() {
        _isAuthenticated = false;

        var user = Unauthenticated;

        try {
            var userResponse = await _httpClient.GetAsync("manage/info");

            userResponse.EnsureSuccessStatusCode();

            // user is authenticated so builde their authenticated identity
            var userJson = await userResponse.Content.ReadAsStringAsync();
            var userInfo = JsonSerializer.Deserialize<UserInfo>(userJson, _jsonSerializerOptions);

            if (userInfo != null) {
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, userInfo.Email),
                    new Claim(ClaimTypes.Email, userInfo.Email),
                };

                claims.AddRange(
                    userInfo.Claims.Where(c => c.Key != ClaimTypes.Name && c.Key != ClaimTypes.Email)
                    .Select(c => new Claim(c.Key, c.Value)));

                var id = new ClaimsIdentity(claims, nameof(CookieAuthenticationStateProvider));
                user = new ClaimsPrincipal(id);
                _isAuthenticated = true;
            }
        } catch { }

        return new AuthenticationState(user);
    }

    public async Task LogoutAsync() {
        await _httpClient.PostAsync("logout", null);
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task<bool> CheckAuthenticatedAsync() {
        await GetAuthenticationStateAsync();
        return _isAuthenticated;
    }
}
