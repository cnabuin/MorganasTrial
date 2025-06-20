namespace UmbracoBridge.Services;

public interface IUmbracoService
{
    Task<object?> GetContent();
    Task<object?> GetContent(string id);
    Task<object?> GetHealthChecks();
    Task<object?> IsOk(bool isOk);
}
