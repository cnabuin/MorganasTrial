using Duende.IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace UmbracoBridge.Services;

public class UmbracoManagementService : IUmbracoManagementService
{
    private const string umbracoManagementUrl = "https://localhost:5001/umbraco/management/api/v1";

    private readonly IHttpClientFactory _httpClientFactory;
    public UmbracoManagementService(IHttpClientFactory httpClientFactory)
    {

        _httpClientFactory = httpClientFactory;
    }

    public async Task<object?> GetHealthChecks()
    {
        HttpClient client = _httpClientFactory.CreateClient();
        string url = $"{umbracoManagementUrl}/health-check-group";

        string token = await GetAuthToken();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        HttpResponseMessage response = await client.GetAsync(url);

        string responseContent = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            return JsonSerializer.Deserialize<object>(responseContent);
        }
        else
        {
            ProblemDetails? problemDetails = string.IsNullOrWhiteSpace(responseContent)
                            ? null
                            : JsonSerializer.Deserialize<ProblemDetails>(responseContent);

            throw new ApiException((int)response.StatusCode, problemDetails);
        }
    }

    public async Task<string?> Create(CreateDocumentTypeRequestModel value)
    {
        HttpClient client = _httpClientFactory.CreateClient();
        string url = $"{umbracoManagementUrl}/document-type";

        string token = await GetAuthToken();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        StringContent content = new(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PostAsync(url, content);
        string responseContent = await response.Content.ReadAsStringAsync();

        if (response.StatusCode == System.Net.HttpStatusCode.Created)
        {
            response.Headers.TryGetValues("Umb-Generated-Resource", out var generatedResource);

            return generatedResource?.FirstOrDefault();
        }
        else
        {
            ProblemDetails? problemDetails = string.IsNullOrWhiteSpace(responseContent) 
                ? null 
                : JsonSerializer.Deserialize<ProblemDetails>(responseContent);

            throw new ApiException((int)response.StatusCode, problemDetails);
        }
    }

    public async Task Delete(string id)
    {
        HttpClient client = _httpClientFactory.CreateClient();
        string url = $"{umbracoManagementUrl}/document-type/{id}";

        string token = await GetAuthToken();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        HttpResponseMessage response = await client.DeleteAsync(url);
        string responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            ProblemDetails? problemDetails = string.IsNullOrWhiteSpace(responseContent)
                ? null
                : JsonSerializer.Deserialize<ProblemDetails>(responseContent);

            throw new ApiException((int)response.StatusCode, problemDetails);
        }
    }

    private async Task<string> GetAuthToken()
    {
        HttpClient client = _httpClientFactory.CreateClient();

        TokenResponse tokenResponse = await client.RequestClientCredentialsTokenAsync(
            new ClientCredentialsTokenRequest
            {
                Address = $"{umbracoManagementUrl}/security/back-office/token",
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
