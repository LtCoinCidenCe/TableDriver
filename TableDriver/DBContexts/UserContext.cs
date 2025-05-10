using Microsoft.EntityFrameworkCore;
using TableDriver.Models.Blog;
using TableDriver.Models.User;
using TableDriver.Models.User.Augmentations;
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
        string dbConnectionString = $"Host={Env.DATABASEURL};Username=dbuser;Password=mysecretpassword;Database=TableDriver";
        optionsBuilder.UseNpgsql($"Host={Env.DATABASEURL};Username=dbuser;Password=mysecretpassword;Database=TableDriver");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().Property(e => e.CreatedAt).HasDefaultValueSql("now()");
    }

    public DbSet<User> User => Set<User>();
    public DbSet<UserDisplayNameHistory> UserDisplayNameHistory => Set<UserDisplayNameHistory>();
    public DbSet<UserMemory> UserMemory => Set<UserMemory>();
    public DbSet<Blog> Blog => Set<Blog>();
}
