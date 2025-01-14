using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TableDriver.Models.Misc;
using Microsoft.EntityFrameworkCore;

namespace TableDriver.Models
{
    // This dictates the database columns
    public abstract class UserBase:UserNonSensitive
    {
        public ulong ID { get; set; }

        [MaxLength(32)]
        public byte[] Passhash { get; set; } = new byte[32];

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime LastUpdatedAt { get; set; }

        public Gender Gender { get; set; } = Gender.secret;

        [Obsolete(message:"It doesn't work")]
        public T TransformToType<T>() where T : UserBase
        {
            // can't do this
            throw new NotImplementedException();
            // return this as T;
        }

        public T CloneToType<T>() where T : UserBase, new()
        {
            return new T()
            {
                ID = ID,
                Username = Username,
                DisplayName = DisplayName,
                Gender = Gender,
                Introduction = Introduction,
                Passhash = Passhash,
                CreatedAt = CreatedAt,
                LastUpdatedAt = LastUpdatedAt,
            };
        }
    }
}
