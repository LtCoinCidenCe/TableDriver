using Microsoft.EntityFrameworkCore;
using TableDriver.Models;
using TableDriver.Utilities;

namespace TableDriver.DBContexts;

public class UserContext : DbContext
{
    ILogger<UserContext> logger;
    string connectionString = $"server={Env.DATABASEURL};port=3306;database=bloglist;uid=root;password=mysecretpassword;SslMode=Disabled";
    public UserContext(
            DbContextOptions<UserContext> options,
            ILogger<UserContext> dilogger) : base(options)
    {
        logger = dilogger;
    }

    public DbSet<User> User => Set<User>();
}
