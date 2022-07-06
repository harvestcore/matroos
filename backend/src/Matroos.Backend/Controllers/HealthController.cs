using Matroos.Resources.Extensions;

using Microsoft.AspNetCore.Mvc;

namespace Matroos.Backend.Controllers;

[ApiController]
[Route("health")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public object Get()
    {
        AssemblyInfo assemblyInfo = AssemblyExtensions.GetAssemblyInfo();
        return Ok(new
        {
            Ok = true,
            assemblyInfo.Version,
        });
    }
}
