using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using TableDriver.Models.Misc;

namespace TableDriver.Models.User
{
    /// <summary>
    /// This is for endpoints only
    /// </summary>
    public class UserNew : UserNonSensitive
    {
        [MinLength(3)]
        [MaxLength(100)]
        public string Password { get; set; } = string.Empty;

        public Gender Gender { get; set; } = Gender.secret;

        public User ToUserForStorage(PasswordHasher<UserBase> passwordHasher)
        {
            User entity = new()
            {
                Username = Username,
                DisplayName = DisplayName,
                Introduction = Introduction,
                Gender = Gender,
            };
            entity.Passhash = passwordHasher.HashPassword(entity, Password);
            return entity;
        }
    }
}
