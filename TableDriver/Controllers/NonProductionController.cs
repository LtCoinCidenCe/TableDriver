#if DEBUG
using Microsoft.AspNetCore.Mvc;
using TableDriver.DBContexts;

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
    }
}
#endif
