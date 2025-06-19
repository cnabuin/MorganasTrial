using Microsoft.AspNetCore.Mvc;
using UmbracoBridge.Services;

namespace UmbracoBridge.Controllers;

[Route("[controller]")]
[ApiController]
public class HealthcheckController : ControllerBase
{
    private readonly IUmbracoManagementService _umbracoService;
    public HealthcheckController(IUmbracoManagementService umbracoService)
    {
        _umbracoService =  umbracoService;
    }

    // GET: /HealthCheck
    [HttpGet]
    public async Task<ObjectResult> Get()
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
}
