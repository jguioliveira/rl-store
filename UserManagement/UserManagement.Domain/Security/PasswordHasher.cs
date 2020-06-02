using System;
using System.Linq;
using System.Security.Cryptography;

namespace UserManagement.Domain.Security
{
    internal static class PasswordHasher
    {        
        internal static string HashPassword(string password)
        {
            const int iterations = 1000; // default for Rfc2898DeriveBytes
            const int subkeyLength = 256 / 8; // 256 bits
            const int saltSize = 128 / 8; // 128 bits

            var rng = new RNGCryptoServiceProvider();
            byte[] salt = new byte[saltSize];
            rng.GetBytes(salt);

            using var deriveBytes = new Rfc2898DeriveBytes(password, salt, iterations);
            byte[] subkey = deriveBytes.GetBytes(subkeyLength);

            var outputBytes = new byte[saltSize + subkeyLength];
            Buffer.BlockCopy(salt, 0, outputBytes, 0, saltSize);
            Buffer.BlockCopy(subkey, 0, outputBytes, saltSize, subkeyLength);

            return Convert.ToBase64String(outputBytes);
        }

        internal static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            const int iterations = 1000; // default for Rfc2898DeriveBytes
            const int subkeyLength = 256 / 8; // 256 bits
            const int saltSize = 128 / 8; // 128 bits

            byte[] decodedHashedPassword = Convert.FromBase64String(hashedPassword);

            // We know ahead of time the exact length of a valid hashed password payload.
            if (decodedHashedPassword.Length != saltSize + subkeyLength)
            {
                return false; // bad size
            }

            byte[] salt = new byte[saltSize];
            Buffer.BlockCopy(decodedHashedPassword, 0, salt, 0, salt.Length);

            byte[] expectedSubkey = new byte[subkeyLength];
            Buffer.BlockCopy(decodedHashedPassword, salt.Length, expectedSubkey, 0, expectedSubkey.Length);

            // Hash the incoming password and verify it
            using var deriveBytes = new Rfc2898DeriveBytes(password, salt, iterations);
            byte[] actualSubkey = deriveBytes.GetBytes(subkeyLength);

            return actualSubkey.SequenceEqual(expectedSubkey);
        }
    }
}