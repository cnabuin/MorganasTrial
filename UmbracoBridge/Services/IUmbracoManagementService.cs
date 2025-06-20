namespace UmbracoBridge.Services;

public interface IUmbracoManagementService
{
    Task<string?> Create(CreateDocumentTypeRequestModel value);
    Task Delete(string id);
    Task<object?> GetHealthChecks();

}
