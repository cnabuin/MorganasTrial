using Microsoft.AspNetCore.Mvc;
using UmbracoBridge.Services;

namespace UmbracoBridge.Controllers;
[Route("[controller]")]
[ApiController]
public class DocumentTypeController : ControllerBase
{
    private readonly IDocumentTypeService _documentTypeService;

    public DocumentTypeController(IDocumentTypeService documentTypeService)
    {
        _documentTypeService = documentTypeService;
    }

    // POST /DocumentType
    [HttpPost]
    public async Task<ObjectResult> Post([FromBody] CreateDocumentTypeRequestModel value)
    {
        try
        {
            return Ok(await _documentTypeService.Create(value));
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
            await _documentTypeService.Delete(id);
            return Ok();
        }
        catch (ApiException ex)
        {
            return StatusCode(ex.StatusCode, ex.ProblemDetails);
        }
    }
}
