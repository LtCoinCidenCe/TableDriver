using Microsoft.EntityFrameworkCore;
using TableDriver.Models;
using TableDriver.Utilities;

namespace TableDriver.DBContexts;

public class UserContext : DbContext
{
    ILogger<UserContext> logger;

    public UserContext(
            DbContextOptions<UserContext> options,
            ILogger<UserContext> dilogger) : base(options)
    {
        logger = dilogger;
    }

    public DbSet<User> User => Set<User>();
}
