using Microsoft.AspNetCore.Mvc;

namespace UmbracoBridge.Controllers;
[Route("[controller]")]
[ApiController]
public class DocumentTypeController : ControllerBase
{
    private readonly IUmbracoManagementService _umbracoService;

    public DocumentTypeController(IUmbracoManagementService umbracoService)
    {
        _umbracoService = umbracoService;
    }

    // POST /DocumentType
    [HttpPost]
    public async Task<IResult> Post([FromBody] CreateDocumentTypeRequestModel value)
    {
        return await _umbracoService.Create(value);
    }

    // DELETE /DocumentType/5
    [HttpDelete("{id}")]
    public async Task<IResult> Delete(string id)
    {
        return await _umbracoService.Delete(id);
    }
}
