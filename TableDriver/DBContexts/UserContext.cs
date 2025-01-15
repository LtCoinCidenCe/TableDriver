using Microsoft.EntityFrameworkCore;
using TableDriver.Models.Blog;
using TableDriver.Models.User;
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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string dbConnectionString = $"server={Env.DATABASEURL};port=3306;database=TableDriver;uid=root;password=mysecretpassword;SslMode=Disabled";
        var serverVersion = new MySqlServerVersion(new Version(5, 7, 44));
        optionsBuilder.UseMySql(dbConnectionString, serverVersion).EnableDetailedErrors();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserMemory>().HasTableOption("engine", "MEMORY");
    }

    public DbSet<User> User => Set<User>();
    public DbSet<UserMemory> UserMemory => Set<UserMemory>();
    public DbSet<Blog> Blog => Set<Blog>();
}
