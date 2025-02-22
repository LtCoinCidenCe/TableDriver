using Microsoft.EntityFrameworkCore;
using TableDriver.DBContexts;
using TableDriver.Models.User;

namespace TableDriver.Services;

public class UserService(UserContext userContext)
{
    public List<UserNonSensitive> AllUsers()
    {
        // this is a wonderful query
        // with contains being SQL IN
        // with Select filter being SQL SELECT
        // just so beautiful
        List<long> smallIDs = [ 1, 2, 3, 4, 5, 6, 7, 8 ];
        return userContext.User
            .AsNoTracking()
            .Where(row => smallIDs.Contains(row.ID))
            .Select(row => new UserNonSensitive()
            {
                Username = row.Username,
                DisplayName = row.DisplayName,
                Introduction = row.Introduction
            }).ToList();
    }

    public UserBase? GetUserbyID(string sid)
    {
        if (long.TryParse(sid, out var id))
        {
            UserMemory? inMemory = userContext.UserMemory.SingleOrDefault(u => u.ID == id);
            if (inMemory is not null)
            {
                return inMemory;
            }
            return userContext.User.SingleOrDefault(u => u.ID == id);
        }
        else
        {
            UserMemory? inMemory = userContext.UserMemory.SingleOrDefault(u => u.Username == sid);
            if (inMemory is not null)
            {
                return inMemory;
            }
            return userContext.User.SingleOrDefault(u => u.Username == sid);
        }
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
        // copy id from real database to memory database
        UserMemory userMemory = user.CloneToType<UserMemory>();
        userContext.UserMemory.Add(userMemory);
        userContext.SaveChanges();
        return user;
    }

    public int ModifyIntroduction(string userid, string newIntro)
    {
        // userid can possibly be an id so check first
        if (long.TryParse(userid, out var id))
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
