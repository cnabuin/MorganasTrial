using Microsoft.AspNetCore.Mvc;
using UmbracoBridge.Services;

namespace UmbracoBridge.Controllers;

[Route("[controller]")]
[ApiController]
public class ContentController : ControllerBase
{
    private readonly IUmbracoService _umbracoService;
    public ContentController(IUmbracoService umbracoService)
    {
        _umbracoService =  umbracoService;
    }

    // GET: /HealthCheck
    [HttpGet]
    public async Task<ObjectResult> GetContent()
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

    [HttpGet("{id}")]
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
