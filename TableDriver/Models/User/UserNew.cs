using System.ComponentModel.DataAnnotations;
using TableDriver.Models.Misc;

namespace TableDriver.Models.User
{
    public class UserNew
    {
        [MinLength(3)]
        [RegularExpression("^[A-Za-z][A-Za-z0-9]{4,55}$")]
        public string Username { get; set; } = string.Empty;

        [MinLength(2)]
        [MaxLength(60)]
        public string DisplayName { get; set; } = string.Empty;

        [MinLength(3)]
        [MaxLength(100)]
        public string Password { get; set; } = string.Empty;

        [MaxLength(120)]
        public string Introduction { get; set; } = string.Empty;

        public Gender Gender { get; set; } = Gender.secret;
    }
}
