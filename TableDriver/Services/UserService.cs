using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TableDriver.DBContexts;
using TableDriver.Models.User;
using TableDriver.Models.User.Augmentations;
using TableDriver.Utilities;

namespace TableDriver.Services;

public class UserService(UserContext userContext)
{
    public List<UserNonSensitive> AllUsers()
    {
        // this is a wonderful query
        // with contains being SQL IN
        // with Select filter being SQL SELECT
        // just so beautiful
        List<long> smallIDs = [1, 2, 3, 4, 5, 6, 7, 8];
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

    public int ModifyDisplayName(string userid, string newDisplayName)
    {
        for (int retries = 0; retries < 5; retries++)
        {
            User doll = new() { DisplayName = newDisplayName };
            ValidationContext validationContext = new(doll);
            PropertyInfo? property = typeof(User).GetProperty("DisplayName");
            if (property is null)
                return 99;
            var attrs = property.GetCustomAttributes(true).OfType<ValidationAttribute>();
            var valid = Validator.TryValidateValue(newDisplayName, validationContext, null, attrs);
            if (!valid)
            {
                return 2;
            }

            User? uuu;
            if (long.TryParse(userid, out var id))
            {
                uuu = userContext.User.AsNoTracking().SingleOrDefault(user => user.ID == id);
            }
            else
            {
                uuu = userContext.User.AsNoTracking().SingleOrDefault(user => user.Username == userid);
            }
            if (uuu is null)
            {
                return 0;
            }

            // optimistic concurrency control
            int updates = userContext.User.Where(user => user.ID == id && user.LastUpdatedAt == uuu.LastUpdatedAt)
            .ExecuteUpdate(setter => setter
            .SetProperty(row => row.DisplayName, newDisplayName)
            .SetProperty(row => row.LastUpdatedAt, DateTime.UtcNow)
            );
            if (updates == 0)
            {
                continue;
            }
            if (updates > 1)
            {
                return 99; // db system error
            }

            var history = new UserDisplayNameHistory()
            {
                // User = uuu, // uuu is out of date
                UserID = uuu.ID,
                LastUpdatedAt = uuu.LastUpdatedAt,
                DisplayName = uuu.DisplayName
            };
            userContext.UserDisplayNameHistory.Add(history);
            userContext.SaveChanges();
            return 1; // correct return
        }
        return 5; // too many retries
    }
}
