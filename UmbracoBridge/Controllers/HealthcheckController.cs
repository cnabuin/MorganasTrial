using Microsoft.AspNetCore.Mvc;

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
    public async Task<IResult> Get()
    {
        return await _umbracoService.GetHealthChecks();
    }
}
