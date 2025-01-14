using Microsoft.AspNetCore.Mvc;

namespace TableDriver.Controllers;

[Route("api/[controller]")]
public class DebugController(ILogger<DebugController> logger) : ControllerBase
{

}
