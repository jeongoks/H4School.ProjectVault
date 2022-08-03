using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace H4School.ProjectVault.Service.Services
{
    public class PasswordHandler
    {
        public static byte[] GenerateSalt()
        {
            const int saltLength = 32;

            using RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
            byte[] saltedValue = new byte[saltLength];
            randomNumberGenerator.GetBytes(saltedValue);

            return saltedValue;
        }

        public static byte[] Combine(byte[] password, byte[] salt)
        {
            byte[] combinedResult = new byte[password.Length + salt.Length];

            Buffer.BlockCopy(password, 0, combinedResult, 0, password.Length);
            Buffer.BlockCopy(salt, 0, combinedResult, password.Length, salt.Length);

            return combinedResult;
        }

        public static byte[] HashingPasswordWithSalt(byte[] toBeHashed, byte[] salt)
        {
            using SHA256 sha256 = SHA256.Create();
            return sha256.ComputeHash(Combine(toBeHashed, salt));
        }
    }
}
