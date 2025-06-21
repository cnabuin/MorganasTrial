using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using UmbracoBridge.Services;

namespace UmbracoBridge.Controllers;

[Route("[controller]")]
[ApiController]
public class ContentController : ControllerBase
{
    private readonly IUmbracoManagementService _umbracoService;
    public ContentController(IUmbracoManagementService umbracoService)
    {
        _umbracoService = umbracoService;
    }

    /// <summary>
    /// Retrieves all content.
    /// </summary>
    /// <returns>A collection of content items.</returns>
    [HttpGet]
    [ProducesResponseType<object>(StatusCodes.Status200OK)]
    public async Task<ObjectResult> GetAllContent()
    {
        try
        {
            object? response = await _umbracoService.GetContent();
            return Ok(response);
        }
        catch (ApiException ex)
        {
            return StatusCode(ex.StatusCode, ex.ProblemDetails);
        }
    }

    /// <summary>
    /// Retrieves a specific content item by its identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the content item to retrieve.</param>
    /// <returns>The requested content item if found.</returns>
    /// <response code="200">Returns the requested content item successfully.</response>
    /// <response code="404">If the content item was not found.</response>
    [HttpGet("{id}")]
    [ProducesResponseType<object>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ObjectResult> GetContentById(string id)
    {
        try
        {
            object? response = await _umbracoService.GetContent(id);
            return Ok(response);
        }
        catch (ApiException ex)
        {
            return StatusCode(ex.StatusCode, ex.ProblemDetails);
        }
    }
}
