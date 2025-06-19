using Microsoft.AspNetCore.Mvc;
using UmbracoBridge.Services;

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
    public async Task<ObjectResult> Post([FromBody] CreateDocumentTypeRequestModel value)
    {
        try
        {
            return Ok(await _umbracoService.Create(value));
        }
        catch (ApiException ex)
        {
            return StatusCode(ex.StatusCode, ex.ProblemDetails);
        }
    }

    // DELETE /DocumentType/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        try
        {
            await _umbracoService.Delete(id);
            return Ok();
        }
        catch (ApiException ex)
        {
            return StatusCode(ex.StatusCode, ex.ProblemDetails);
        }
    }
}
