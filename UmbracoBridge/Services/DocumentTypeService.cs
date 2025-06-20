using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace UmbracoBridge.Services;

public class DocumentTypeService : IDocumentTypeService
{
    private readonly HttpClient _client;
    private readonly IAuthService _authService;

    public DocumentTypeService(HttpClient client, IAuthService authService)
    {
        _client = client;
        _authService = authService;
    }

    public async Task<string?> Create(CreateDocumentTypeRequestModel value)
    {
        string url = $"{_client.BaseAddress}umbraco/management/api/v1/document-type";

        string token = await _authService.GetAuthToken();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        StringContent content = new(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _client.PostAsync(url, content);
        string responseContent = await response.Content.ReadAsStringAsync();

        if (response.StatusCode == System.Net.HttpStatusCode.Created)
        {
            response.Headers.TryGetValues("Umb-Generated-Resource", out IEnumerable<string>? generatedResource);

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
        string url = $"{_client.BaseAddress}umbraco/management/api/v1/document-type/{id}";

        string token = await _authService.GetAuthToken();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        HttpResponseMessage response = await _client.DeleteAsync(url);
        string responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            ProblemDetails? problemDetails = string.IsNullOrWhiteSpace(responseContent)
                ? null
                : JsonSerializer.Deserialize<ProblemDetails>(responseContent);

            throw new ApiException((int)response.StatusCode, problemDetails);
        }
    }
}
