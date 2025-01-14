using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using TableDriver.Models;
using TableDriver.Models.Misc;
using TableDriver.Services;

namespace TableDriver.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(ILogger<UserController> logger, UserService userService) : ControllerBase
{
    [HttpGet]
    public IEnumerable<User> GetAllUsers()
    {
        logger.LogInformation(Request.GetDisplayUrl());
        return userService.AllUsers();
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

public class UserNew
{
    [MinLength(3)]
    [RegularExpression("^[A-Za-z][A-Za-z0-9]{4,55}$")]
    public string Username { get; set; } = string.Empty;

    [MinLength(2)]
    [MaxLength(60)]
    public string DisplayName { get; set; } = string.Empty;

    [MinLength(3)]
    [MaxLength(100)]
    public string Password { get; set; } = string.Empty;

    [MaxLength(120)]
    public string Introduction { get; set; } = string.Empty;

    public Gender Gender { get; set; } = Gender.secret;
}
