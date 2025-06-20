using Duende.IdentityModel.Client;

namespace UmbracoBridge.Services;

public class AuthService : IAuthService
{
    private const string securityEndpoint = "umbraco/management/api/v1/security";
    private readonly HttpClient _client;

    public AuthService(HttpClient httpClient)
    {
        _client = httpClient;
    }

    public async Task<string> GetAuthToken()
    {
        TokenResponse tokenResponse = await _client.RequestClientCredentialsTokenAsync(
            new ClientCredentialsTokenRequest
            {
                Address = $"{_client.BaseAddress}{securityEndpoint}/back-office/token",
                ClientId = "umbraco-back-office-admin",
                ClientSecret = "admin12345"
            }
        );

        if (tokenResponse.IsError || tokenResponse.AccessToken is null)
        {
            return string.Empty;
        }

        return tokenResponse.AccessToken;
    }

}
