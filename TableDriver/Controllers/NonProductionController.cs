#if DEBUG
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
            List<User> users = [
                new User(){
                    Username="firstUser",
                    DisplayName="FirstGuy",
                    Gender= Gender.male,
                    Introduction="The first person to be on the tables.",
                    Passhash= "theno.1"},
                new User(){
                    Username="fernando",
                    DisplayName="Fernando",
                    Gender= Gender.male,
                    Introduction="Wow",
                    Passhash= "alonso"},
                new User(){
                    Username="kimiraikkonen",
                    DisplayName="Kimi Räikkönen",
                    Gender= Gender.male,
                    Introduction="Finnish F1 driver",
                    Passhash= "eimuuta"},
                ];
            users.ForEach(oneUser => oneUser.Passhash = passwordHasher.HashPassword(oneUser, oneUser.Passhash));
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
