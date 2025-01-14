using TableDriver.Models.Misc;

namespace TableDriver.Models
{
    public class User
    {
        public ulong ID { get; set; }
        public string Username { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public byte[] Passhash { get; set; } = new byte[32];
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public string Introduction { get; set; } = string.Empty;
        public Gender Gender { get; set; }
    }
}
