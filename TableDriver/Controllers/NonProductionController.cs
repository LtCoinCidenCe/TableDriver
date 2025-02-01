#if DEBUG
using Microsoft.AspNetCore.Mvc;
using TableDriver.DBContexts;
using TableDriver.Models.Misc;
using TableDriver.Models.User;
using TableDriver.Utilities;

namespace TableDriver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NonProductionController(UserContext userContext) : ControllerBase
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
                    Passhash= PasswordHashing.GetBytes("theno.1")},
                new User(){
                    Username="fernando",
                    DisplayName="Fernando",
                    Gender= Gender.male,
                    Introduction="Wow",
                    Passhash= PasswordHashing.GetBytes("alonso")},
                new User(){
                    Username="kimiraikkonen",
                    DisplayName="Kimi Räikkönen",
                    Gender= Gender.male,
                    Introduction="Finnish F1 driver",
                    Passhash= PasswordHashing.GetBytes("eimuuta")},
                ];
            userContext.User.AddRange(users);
            userContext.SaveChanges();
            UserNonSensitive kimi = users[2].GetDataTransferObject();
            return users.ToArray();
        }
    }
}
#endif
