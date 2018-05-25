using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Portal.Apis.Core.Security
{
    internal class PasswordPolicy
    {
        internal static byte[] HashPassword(String password, String salt)
        {
            var combinedPassword = String.Concat(password, salt);
            var sha256 = new SHA256Managed();
            var bytes = UTF8Encoding.UTF8.GetBytes(combinedPassword);
            var hash = sha256.ComputeHash(bytes);
            return hash;
        }

        internal static string HashPasswordAsString(String password, String salt)
        {
            var combinedPassword = String.Concat(password, salt);
            var sha256 = new SHA256Managed();
            var bytes = UTF8Encoding.UTF8.GetBytes(combinedPassword);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        internal static string GetRandomSalt(int size = 12)
        {
            var random = new RNGCryptoServiceProvider();
            var salt = new byte[size];
            random.GetBytes(salt);
            return Convert.ToBase64String(salt);
        }

        internal static bool ValidatePassword(string enteredPassword, byte[] storedHash, string storedSalt)
        {
            var hash = HashPassword(enteredPassword, storedSalt);
            return StructuralComparisons.StructuralEqualityComparer.Equals(storedHash, hash);
        }
    }
}