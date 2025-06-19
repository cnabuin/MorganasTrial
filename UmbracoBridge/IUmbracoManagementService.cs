namespace UmbracoBridge;

public interface IUmbracoManagementService
{
    Task<IResult> Create(CreateDocumentTypeRequestModel value);
    Task<IResult> Delete(string id);
    Task<IResult> GetHealthChecks();
}