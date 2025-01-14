using Microsoft.EntityFrameworkCore;
using TableDriver.DBContexts;
using TableDriver.Models;

namespace TableDriver.Services;

public class UserService(UserContext userContext)
{
    public List<User> AllUsers()
    {
        return userContext.User.AsNoTracking().ToList();
    }

    public User? GetUserbyID(ulong id)
    {
        return userContext.User.SingleOrDefault(u => u.ID == id);
    }

    public User CreateNewUser(User user)
    {
        userContext.User.Add(user);
        userContext.SaveChanges();
        return user;
    }
}
