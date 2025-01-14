using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TableDriver.Models
{
    [Index(nameof(Username), IsUnique = true)]
    [Index("DisplayName")]
    public class UserNonSensitive
    {
        [MaxLength(57)]
        [RegularExpression("^[A-Za-z][A-Za-z0-9]{4,55}$")]
        public string Username { get; set; } = string.Empty;

        [MaxLength(60)]
        public string DisplayName { get; set; } = string.Empty;


        [MaxLength(120)]
        public string Introduction { get; set; } = string.Empty;
    }
}
