using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace UmbracoBridge.Services;

public class UmbracoManagementService : IUmbracoManagementService
{
    private readonly HttpClient _client;
    private readonly IAuthService _authService;

    public UmbracoManagementService(HttpClient client, IAuthService authService)
    {
        _client = client;
        _authService = authService;
    }

    public async Task<object?> GetContent()
    {
        string url = $"{_client.BaseAddress}umbraco/delivery/api/v2/content";

        var apiKey = Environment.GetEnvironmentVariable("UMBRACO_DELIVERY_API_KEY");
        _client.DefaultRequestHeaders.Add("api-key", apiKey);

        HttpResponseMessage response = await _client.GetAsync(url);

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

    public async Task<object?> GetContent(string id)
    {
        string url = $"{_client.BaseAddress}umbraco/delivery/api/v2/content/item/{id}";

        var apiKey = Environment.GetEnvironmentVariable("UMBRACO_DELIVERY_API_KEY");
        _client.DefaultRequestHeaders.Add("api-key", apiKey);

        HttpResponseMessage response = await _client.GetAsync(url);

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

    public async Task<object?> GetHealthChecks()
    {
        string url = $"{_client.BaseAddress}umbraco/management/api/v1/health-check-group";

        string token = await _authService.GetAuthToken();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        HttpResponseMessage response = await _client.GetAsync(url);

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

    public async Task<object?> IsOk(bool isOk)
    {
        string url = $"{_client.BaseAddress}umbraco/management/api/v1/is-ok?isOk={isOk}";

        string token = await _authService.GetAuthToken();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        HttpResponseMessage response = await _client.GetAsync(url);

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
}