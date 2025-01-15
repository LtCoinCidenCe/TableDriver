using Microsoft.EntityFrameworkCore;
using TableDriver.Models.User;

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

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    base.OnConfiguring(optionsBuilder);
    //}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserMemory>().HasTableOption("engine", "MEMORY");
    }

    public DbSet<User> User => Set<User>();
    public DbSet<UserMemory> UserMemory => Set<UserMemory>();
}
