using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TableDriver.Models.Misc;

namespace TableDriver.Models.User
{
    // This dictates the database columns
    public abstract class UserBase : UserNonSensitive, Common
    {
        public ulong ID { get; set; }

        // PasswordHasher gives output length 84
        [MaxLength(85)]
        public string Passhash { get; set; } = string.Empty;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime LastUpdatedAt { get; set; }

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
