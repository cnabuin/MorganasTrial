using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Api.Management.Controllers;
using Umbraco.Cms.Api.Management.Routing;

namespace UmbracoCMS;

[VersionedApiBackOfficeRoute("is-ok")]
[ApiExplorerSettings(GroupName = "Morgana")]
public class MorganaController : ManagementApiControllerBase
{
    [HttpGet]
    public IActionResult CheckIsOk([FromQuery] bool isOk)
    {
        return isOk ? Ok("It's OK") : BadRequest("It is not OK");
    }
}
