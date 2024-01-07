using System.Net;
using System.Net.Http.Headers;
using TeachPlanner.BlazorClient.Services;

namespace TeachPlanner.BlazorClient.Handlers;

public class AuthenticationHandler : DelegatingHandler {
    private readonly IAuthenticationService _authenticationService;
    private readonly IConfiguration _configuration;
    private bool _refreshing;

    public AuthenticationHandler(IAuthenticationService authenticationService, IConfiguration configuration) {
        _authenticationService = authenticationService;
        _configuration = configuration;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {
        var jwt = await _authenticationService.GetJwt();
        var isToServer = request.RequestUri?.AbsoluteUri.StartsWith(_configuration["ServerUrl"] ?? "") ?? false;

        if (isToServer && !string.IsNullOrEmpty(jwt))
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

        var response = await base.SendAsync(request, cancellationToken);

        if (!_refreshing && !string.IsNullOrEmpty(jwt) && response.StatusCode == HttpStatusCode.Unauthorized) {
            try {
                _refreshing = true;

                if (await _authenticationService.Refresh()) {
                    jwt = await _authenticationService.GetJwt();

                    if (isToServer && !string.IsNullOrEmpty(jwt))
                        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

                    response = await base.SendAsync(request, cancellationToken);
                }
            } finally {
                _refreshing = false;
            }
        }

        return response;
    }
}
