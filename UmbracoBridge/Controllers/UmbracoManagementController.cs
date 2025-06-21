using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using UmbracoBridge.Services;

namespace UmbracoBridge.Controllers;

[ApiController]
[Route("")]
public class UmbracoManagementController : ControllerBase
{
    private readonly IUmbracoManagementService _umbracoService;
    private readonly IDocumentTypeService _documentTypeService;

    public UmbracoManagementController(IUmbracoManagementService umbracoService, IDocumentTypeService documentTypeService)
    {
        _umbracoService = umbracoService;
        _documentTypeService = documentTypeService;
    }

    /// <summary>
    /// Performs a health check.
    /// </summary>
    /// <returns>The health check results indicating the system's status.</returns>
    /// <response code="200">Returns the health check results successfully.</response>
    [HttpGet("Healthcheck")]
    [ProducesResponseType<object>(StatusCodes.Status200OK)]
    public async Task<ActionResult> Get()
    {
        try
        {
            object? response = await _umbracoService.GetHealthChecks();
            return Ok(response);
        }
        catch (ApiException ex)
        {
            return StatusCode(ex.StatusCode, ex.ProblemDetails);
        }
    }

    /// <summary>
    /// Validates the service status based on input.
    /// </summary>
    /// <param name="isOk">A boolean used to validate status. Defaults to false if not specified.</param>
    /// <returns>The validation result.</returns>
    /// <response code="200">Validation completed successfully.</response>
    /// <response code="400">If the validation parameters are invalid.</response>
    [HttpGet("Validate")]
    [ProducesResponseType<object>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> IsOk([FromQuery] bool isOk = false)
    {
        try
        {
            object? response = await _umbracoService.IsOk(isOk);
            return Ok(response);
        }
        catch (ApiException ex)
        {
            return StatusCode(ex.StatusCode, ex.ProblemDetails);
        }
    }

    /// <summary>
    /// Creates a new document type.
    /// </summary>
    /// <param name="requestModel">The document type configuration to create.</param>
    /// <returns>The identifier of the newly created document type.</returns>
    /// <response code="200">Document type created successfully.</response>
    /// <response code="400">If the request model is invalid.</response>
    [HttpPost("DocumentType")]
    [ProducesResponseType<string>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ObjectResult> Post([FromBody] CreateDocumentTypeRequestModel requestModel)
    {
        try
        {
            return Ok(await _documentTypeService.Create(requestModel));
        }
        catch (ApiException ex)
        {
            return StatusCode(ex.StatusCode, ex.ProblemDetails);
        }
    }

    /// <summary>
    /// Deletes a document type.
    /// </summary>
    /// <param name="id">The unique identifier of the document type to delete.</param>
    /// <returns>A status code indicating the result of the deletion operation.</returns>
    /// <response code="200">Document type deleted successfully.</response>
    /// <response code="404">If the document type was not found.</response>
    [HttpDelete("DocumentType/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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
