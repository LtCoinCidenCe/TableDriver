using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TableDriver.Models.User;
using TableDriver.Services;

namespace TableDriver.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(ILogger<UserController> logger, UserService userService, BlogService blogService, PasswordHasher<UserBase> passwordHasher) : ControllerBase
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

    [HttpGet("{id}/blogs")]
    public IActionResult GetBlogByAuthor(string id)
    {
        List<Models.Blog.BlogNoContent> result = blogService.GetBlogsTitleByAuthor(id);
        return Ok(result);
    }

    [HttpPost]
    public IActionResult CreateNewUser(UserNew newUser)
    {
        User entity = newUser.ToUserForStorage(passwordHasher);
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

    [HttpPatch("{userid}/displayname")]
    public IActionResult ModifyDisplayName([FromRoute] string userid, string newDisplayName)
    {
        int result = userService.ModifyDisplayName(userid, newDisplayName);
        if (result == 0)
        {
            return NotFound();
        }
        else if (result == 1)
        {
            return Ok();
        }
        else if (result == 2)
        {
            return BadRequest();
        }
        throw new Exception("modifydisplayname guard, too many updated rows");
    }
}
