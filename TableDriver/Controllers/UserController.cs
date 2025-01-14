using Microsoft.AspNetCore.Mvc;
using TableDriver.Models;

namespace TableDriver.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(ILogger<UserController> logger) : ControllerBase
{
    [HttpGet]
    public IEnumerable<User> GetAllUsers()
    {
        return new List<User>() { new User() };
    }

    [HttpPost]
    public IActionResult CreateNewUser()
    {
        return Ok();
    }
}
