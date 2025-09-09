// Backend/Helpers/PasswordHasher.cs
using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace ShopForHomeBackend.Helpers
{
    public static class PasswordHasher
    {
        // Hash password with salt using PBKDF2
        public static string HashPassword(string password)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password, salt, KeyDerivationPrf.HMACSHA256, 10000, 256 / 8));

            return $"{Convert.ToBase64String(salt)}.{hashed}";

        }

        // Verify provided password against stored hash
        public static bool VerifyPassword(string hashedPasswordWithSalt, string password)
        {
            var parts = hashedPasswordWithSalt.Split('.');
            if (parts.Length != 2) return false;

            var salt = Convert.FromBase64String(parts[0]);
            var storedHash = parts[1];

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password, salt, KeyDerivationPrf.HMACSHA256, 10000, 256 / 8));

            return hashed == storedHash;
        }
    }
}
