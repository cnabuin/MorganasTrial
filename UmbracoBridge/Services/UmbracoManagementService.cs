namespace UmbracoBridge.Services;

public abstract class UmbracoManagementService
{
    protected HttpClient _client;
    protected UmbracoManagementService(IHttpClientFactory httpClientFactory)
    {
        _client = httpClientFactory.CreateClient(nameof(UmbracoManagementService));
    }
}
