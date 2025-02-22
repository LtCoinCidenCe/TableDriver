#if DEBUG
using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TableDriver.DBContexts;
using TableDriver.Models.Misc;
using TableDriver.Models.User;

namespace TableDriver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NonProductionController(UserContext userContext, PasswordHasher<UserBase> passwordHasher) : ControllerBase
    {
        [HttpDelete]
        public ActionResult ClearInitDatabase()
        {
            userContext.Database.EnsureDeleted();
            userContext.Database.EnsureCreated();
            return NoContent();
        }

        [HttpPost]
        public UserNonSensitive[] SeedingDatabase()
        {
            // var ddir = Directory.GetCurrentDirectory();
            FileStream fileStream = new("MOCK_DATA.json", FileMode.Open, FileAccess.Read);
            List<User>? users = JsonSerializer.Deserialize<List<User>>(fileStream);
            if(users is null)
            {
                throw new Exception("mock json data returns null");
            }
            users.ForEach(oneUser =>
            {
                oneUser.Passhash = passwordHasher.HashPassword(oneUser, oneUser.Passhash);
                oneUser.CreatedAt = DateTime.UtcNow;
                oneUser.LastUpdatedAt = oneUser.CreatedAt;
            });
            userContext.User.AddRange(users);
            userContext.SaveChanges();
            UserNonSensitive kimi = users[2].GetDataTransferObject();
            return users.ToArray();
        }

        [HttpGet]
        public string GetHash([FromQuery] string pswd)
        {
            return passwordHasher.HashPassword(null, pswd);
        }
    }
}
#endif
