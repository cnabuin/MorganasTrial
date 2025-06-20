namespace UmbracoBridge.Services;

public interface IDocumentTypeService
{
    Task<string?> Create(CreateDocumentTypeRequestModel value);
    Task Delete(string id);
}
