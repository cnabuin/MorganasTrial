using Duende.IdentityModel.Client;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace UmbracoBridge;

public class UmbracoManagementService : IUmbracoManagementService
{
    private const string host = "https://localhost:44353/umbraco/management/api/v1";

    private readonly IHttpClientFactory _httpClientFactory;
    public UmbracoManagementService(IHttpClientFactory httpClientFactory)
    {

        _httpClientFactory = httpClientFactory;
    }

    public async Task<IResult> GetHealthChecks()
    {
        HttpClient client = _httpClientFactory.CreateClient();
        string url = $"{host}/health-check-group";

        string token = await GetAuthToken();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        HttpResponseMessage response = await client.GetAsync(url);
        string responseBody = await response.Content.ReadAsStringAsync();

        return Results.Json(responseBody);
    }

    public async Task<IResult> Create(CreateDocumentTypeRequestModel value)
    {
        HttpClient client = _httpClientFactory.CreateClient();
        string url = $"{host}/document-type";

        string token = await GetAuthToken();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        StringContent content = new (JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
        
        HttpResponseMessage response = await client.PostAsync(url, content);
        string responseBody = await response.Content.ReadAsStringAsync();

        return Results.Json(responseBody);

    }

    public async Task<IResult> Delete(string id)
    {
        HttpClient client = _httpClientFactory.CreateClient();
        string url = $"{host}/document-type/{id}";
        
        string token = await GetAuthToken();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        HttpResponseMessage response = await client.DeleteAsync(url);

        string responseBody = await response.Content.ReadAsStringAsync();
        return Results.Json(responseBody);

    }

    private async Task<string> GetAuthToken()
    {
        HttpClient client = _httpClientFactory.CreateClient();

        TokenResponse tokenResponse = await client.RequestClientCredentialsTokenAsync(
            new ClientCredentialsTokenRequest
            {
                Address = $"{host}/security/back-office/token",
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

