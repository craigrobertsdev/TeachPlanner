using System.Net;
using TeachPlanner.Shared.Domain.Users;

namespace TeachPlanner.BlazorClient.Services;

public class UserService {
    private readonly HttpClient _httpClient;
    private readonly AuthenticationManager _authenticationManager;

    public UserService(HttpClient httpClient, AuthenticationManager authenticationManager) {
        _httpClient = httpClient;
        _authenticationManager = authenticationManager;
    }

    //public async Task<User?> SendAuthenticationRequest(string email, string password) {
    //    var response = await 
    //}
}
