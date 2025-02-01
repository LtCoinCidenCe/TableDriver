using System.Security.Cryptography;
using System.Text;

namespace TableDriver.Utilities
{
    public static class PasswordHashing
    {
        public static byte[] GetBytes(string password)
        {
            return SHA256.HashData(Encoding.UTF8.GetBytes(password + "SuperSalt"));
        }
    }
}
