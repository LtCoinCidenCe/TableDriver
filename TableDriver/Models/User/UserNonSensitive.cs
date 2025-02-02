using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TableDriver.Models.User
{
    // This is shared between database and endpoints
    [Index(nameof(Username), IsUnique = true)]
    [Index("DisplayName")]
    public class UserNonSensitive
    {
        [MinLength(3)]
        [MaxLength(57)]
        [RegularExpression("^[A-Za-z][A-Za-z0-9]{4,55}$")]
        public string Username { get; set; } = string.Empty;

        [MinLength(2)]
        [MaxLength(60)]
        public string DisplayName { get; set; } = string.Empty;


        [MaxLength(120)]
        public string Introduction { get; set; } = string.Empty;

        public UserNonSensitive GetDataTransferObject()
        {
            return new UserNonSensitive
            {
                Username = Username,
                DisplayName = DisplayName,
                Introduction = Introduction,
            };
        }
    }
}
