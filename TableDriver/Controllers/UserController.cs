using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using TableDriver.Models.User;
using TableDriver.Services;

namespace TableDriver.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(ILogger<UserController> logger, UserService userService) : ControllerBase
{
    [HttpGet]
    public IEnumerable<UserNonSensitive> GetAllUsers()
    {
        logger.LogInformation(Request.GetDisplayUrl());
        return userService.AllUsers();
    }

    [HttpGet("{id}")]
    public IActionResult GetUserById(string id)
    {
        UserBase? result = userService.GetUserbyID(id);
        if (result is null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpPost]
    public IActionResult CreateNewUser(UserNew newUser)
    {
        User entity = new()
        {
            Username = newUser.Username,
            DisplayName = newUser.DisplayName,
            Introduction = newUser.Introduction,
            Passhash = SHA256.HashData(Encoding.UTF8.GetBytes(newUser.Password + "SuperSalt")),
            Gender = newUser.Gender,
        };
        User? result = userService.CreateNewUser(entity);
        if (result is null)
        {
            return Conflict();
        }
        return Created("useless header Location", result);
    }

    [HttpPatch("{userid}/introduction")]
    public IActionResult ModifyIntroduction([FromRoute] string userid, string newIntro)
    {
        int updatedRows = userService.ModifyIntroduction(userid, newIntro);
        if (updatedRows == 0)
        {
            return NotFound();
        }
        else if (updatedRows == 1)
        {
            return NoContent();
        }
        // should never get here
        throw new Exception("should not update so many users' introduction");
    }
}
