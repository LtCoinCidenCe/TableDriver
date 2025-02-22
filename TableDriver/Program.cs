using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using TableDriver.DBContexts;
using TableDriver.Models.User;
using TableDriver.Services;
using TableDriver.Utilities;

namespace TableDriver;
public class Program
{
    public static void Main(string[] args)
    {
        // spin up
        string dbConnectionString = $"server={Env.DATABASEURL};port=3306;database=TableDriver;uid=root;password=mysecretpassword;SslMode=Disabled";

        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        //builder.Services.AddDbContext<UserContext>(dbContextOptions =>
        //{
        //    var serverVersion = new MySqlServerVersion(new Version(5, 7, 44));
        //    dbContextOptions.UseMySql(dbConnectionString, serverVersion)
        //    .LogTo(Console.WriteLine, LogLevel.Information)
        //    .EnableSensitiveDataLogging()
        //    .EnableDetailedErrors();
        //});
        builder.Services.AddDbContext<UserContext>();
        builder.Services.AddScoped<UserService>();
        builder.Services.AddScoped<BlogService>();
        builder.Services.AddScoped(factory => new PasswordHasher<UserBase>(Options.Create(new PasswordHasherOptions() { IterationCount = 8 })));

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
