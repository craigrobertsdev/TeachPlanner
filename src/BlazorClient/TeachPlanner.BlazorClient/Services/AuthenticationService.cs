using System.Net.Http;
using System.Net.Http.Json;
using TeachPlanner.Shared.Contracts.Authentication;

namespace TeachPlanner.BlazorClient.Services;

public class AuthenticationService {
    private readonly IHttpClientFactory _factory;

    public AuthenticationService(IHttpClientFactory factory) {
        _factory = factory;
    }
    public async Task<DateTime> Login(LoginRequest request) {
        var response = await _factory.CreateClient("ServerApi").PostAsync("api/auth/login", JsonContent.Create(request));

        if (!response.IsSuccessStatusCode) {
            throw new UnauthorizedAccessException("Login failed");
        }

        var content = await response.Content.ReadFromJsonAsync<AuthenticationResponse>();
        return DateTime.Now;

        //if (content.)
    }
}
