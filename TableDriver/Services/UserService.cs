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

    public User? CreateNewUser(User user)
    {
        // username is unique and failed insertion still does auto increment on id
        userContext.User.Add(user);
        try
        {
            userContext.SaveChanges();
        }
        catch (DbUpdateException dbex)
        {
            if (dbex.InnerException is null)
            {
                throw;
            }
            if (dbex.InnerException.Message.StartsWith("Duplicate entry"))
            {
                return null;
            }
        }
        return user;
    }

    public int ModifyIntroduction(string userid, string newIntro)
    {
        // userid can possibly be an id so check first
        if (ulong.TryParse(userid, out var id))
        {
            int callupdate = userContext.User.Where(user => user.ID == id)
                .ExecuteUpdate(setter => setter.SetProperty(row => row.Introduction, newIntro));
            return callupdate;
        }
        else
        {
            int callupdate = userContext.User.Where(user => user.Username == userid)
                .ExecuteUpdate(setter => setter.SetProperty(row => row.Introduction, newIntro));
            return callupdate;
        }
    }
}
