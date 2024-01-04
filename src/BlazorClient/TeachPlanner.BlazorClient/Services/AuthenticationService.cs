using Blazored.LocalStorage;
using System.Net.Http.Json;
using TeachPlanner.Shared.Contracts.Authentication;
using TeachPlanner.Shared.Contracts.Teachers;

namespace TeachPlanner.BlazorClient.Services;

// TODO: Add an event to notify subscribers when the JWT expires and automatically refresh it
public class AuthenticationService : IAuthenticationService {
    private readonly IHttpClientFactory _factory;
    private ILocalStorageService _localStorage;
    private const string JWT_KEY = nameof(JWT_KEY);

    private string? _tokenCache = null;

    public AuthenticationService(IHttpClientFactory factory, ILocalStorageService localStorage) {
        _factory = factory;
        _localStorage = localStorage;
    }

    public async ValueTask<string> GetJWT() {
        if (string.IsNullOrWhiteSpace(_tokenCache)) {
            _tokenCache = await _localStorage.GetItemAsync<string>(JWT_KEY);
        }
        
        return _tokenCache!;
    }

    public async Task<TeacherModel> Login(LoginModel model) {
        var response = await _factory.CreateClient("ServerApi").PostAsync("api/auth/login", JsonContent.Create(model));

        if (!response.IsSuccessStatusCode) {
            throw new UnauthorizedAccessException("Login failed");
        }

        var content = await response.Content.ReadFromJsonAsync<AuthenticationResponse>();
        if (content == null) {
            throw new InvalidDataException("Invalid response from server");
        }

        await _localStorage.SetItemAsync(JWT_KEY, content.Token);
        return content.Teacher;
    }

    public async Task Logout() {
        await _localStorage.RemoveItemAsync(JWT_KEY);
        _tokenCache = null;
    }
}
