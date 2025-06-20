using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace UmbracoBridge.Services;

public class HealthcheckService : UmbracoManagementService, IHealthCheckService
{
    private readonly IAuthService _authService;
    public HealthcheckService(IHttpClientFactory httpClient, IAuthService authService) : base(httpClient)
    {
        _authService = authService;
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
}