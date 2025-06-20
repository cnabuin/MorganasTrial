using Microsoft.AspNetCore.Mvc;
using UmbracoBridge.Services;

namespace UmbracoBridge.Controllers;

[Route("[controller]")]
[ApiController]
public class HealthcheckController : ControllerBase
{
    private readonly IHealthCheckService _umbracoService;
    public HealthcheckController(IHealthCheckService umbracoService)
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
