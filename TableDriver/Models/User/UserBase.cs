using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TableDriver.Models.Misc;

namespace TableDriver.Models.User
{
    /// <summary>
    /// This dictates the database columns.
    /// </summary>
    public abstract class UserBase : UserNonSensitive, Common
    {
        public long ID { get; set; }

        // PasswordHasher gives output length 84
        [MaxLength(85)]
        public string Passhash { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;

        public Gender Gender { get; set; } = Gender.secret;

        [Obsolete(message: "It doesn't work")]
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
