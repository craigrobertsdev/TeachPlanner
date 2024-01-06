using Blazored.LocalStorage;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Security.Claims;
using TeachPlanner.Shared.Contracts.Authentication;

namespace TeachPlanner.BlazorClient.Services;

public class AuthenticationService : IAuthenticationService {
    private readonly IHttpClientFactory _factory;
    private ILocalStorageService _localStorage;

    private const string JWT_KEY = nameof(JWT_KEY);
    private const string REFRESH_KEY = nameof(REFRESH_KEY);

    private string? _jwtCache;

    public event Action<string?>? LoginChange;

    public AuthenticationService(IHttpClientFactory factory, ILocalStorageService localStorage) {
        _factory = factory;
        _localStorage = localStorage;
    }

    public async ValueTask<string> GetJwtAsync() {
        if (string.IsNullOrEmpty(_jwtCache))
            _jwtCache = await _localStorage.GetItemAsync<string>(JWT_KEY);

        return _jwtCache;
    }

    public async Task LogoutAsync() {
        var response = await _factory.CreateClient("ServerApi").DeleteAsync("api/authentication/revoke");

        await _localStorage.RemoveItemAsync(JWT_KEY);
        await _localStorage.RemoveItemAsync(REFRESH_KEY);

        _jwtCache = null;

        await Console.Out.WriteLineAsync($"Revoke gave response {response.StatusCode}");

        LoginChange?.Invoke(null);
    }

    private static string GetUsername(string token) {
        var jwt = new JwtSecurityToken(token);

        return jwt.Claims.First(c => c.Type == ClaimTypes.Name).Value;
    }

    public async Task<DateTime> LoginAsync(LoginModel model) {
        var response = await _factory.CreateClient("ServerApi").PostAsync("api/authentication/login",
            JsonContent.Create(model));

        if (!response.IsSuccessStatusCode)
            throw new UnauthorizedAccessException("Login failed.");

        var content = await response.Content.ReadFromJsonAsync<AuthenticationResponse>();

        if (content == null)
            throw new InvalidDataException();

        await _localStorage.SetItemAsync(JWT_KEY, content.Token);
        await _localStorage.SetItemAsync(REFRESH_KEY, content.RefreshToken);

        LoginChange?.Invoke(GetUsername(content.Token));

        return content.Expiration;
    }

    public async Task<bool> RefreshAsync() {
        var accessToken = await _localStorage.GetItemAsync<string>(JWT_KEY);
        var refreshToken = await _localStorage.GetItemAsync<string>(REFRESH_KEY);
        var model = new RefreshModel(accessToken, refreshToken);

        var response = await _factory.CreateClient("ServerApi").PostAsync("api/authentication/refresh",
                                                    JsonContent.Create(model));

        if (!response.IsSuccessStatusCode) {
            await LogoutAsync();

            return false;
        }

        var content = await response.Content.ReadFromJsonAsync<AuthenticationResponse>();

        if (content == null)
            throw new InvalidDataException();

        await _localStorage.SetItemAsync(JWT_KEY, content.Token);
        await _localStorage.SetItemAsync(REFRESH_KEY, content.RefreshToken);

        _jwtCache = content.Token;

        return true;
    }
}
