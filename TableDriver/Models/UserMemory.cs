using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TableDriver.Models.Misc;

namespace TableDriver.Models
{
    [Index(nameof(Username), IsUnique = true)]
    [Index("DisplayName")]
    public class UserMemory
    {
        public ulong ID { get; set; }

        [MaxLength(57)]
        [RegularExpression("^[A-Za-z][A-Za-z0-9]{4,55}$")]
        public string Username { get; set; } = string.Empty;

        [MaxLength(60)]
        public string DisplayName { get; set; } = string.Empty;

        [MaxLength(32)]
        public byte[] Passhash { get; set; } = new byte[32];

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime LastUpdatedAt { get; set; }

        [MaxLength(120)]
        public string Introduction { get; set; } = string.Empty;

        public Gender Gender { get; set; } = Gender.secret;
    }
}
