using Duende.IdentityModel.Client;

namespace UmbracoBridge.Services;

public class AuthService : UmbracoManagementService, IAuthService
{
    public AuthService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
    }

    public async Task<string> GetAuthToken()
    {
        TokenResponse tokenResponse = await _client.RequestClientCredentialsTokenAsync(
            new ClientCredentialsTokenRequest
            {
                Address = $"{_client.BaseAddress}umbraco/management/api/v1/security/back-office/token",
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
