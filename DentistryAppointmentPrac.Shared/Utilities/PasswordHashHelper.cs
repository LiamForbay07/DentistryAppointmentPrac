using Microsoft.EntityFrameworkCore.Storage.Json;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Utilities
{
    public static class PasswordHashHelper
    {
        private const int SaltSize = 32;
        private const int HashSize = 32;
        private const int Iterations = 100000;

        public static string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));

            byte[] salt = new byte[SaltSize];
            using(var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            byte[] hash = HashPasswordWithSalt(password, salt);

            byte[] hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(hash, 0, hashBytes, 0, HashSize);
            Array.Copy(salt, 0, hashBytes, HashSize, SaltSize);

            return Convert.ToBase64String(hashBytes);
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            if(string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));

            if (string.IsNullOrEmpty(hashedPassword))
                throw new ArgumentNullException(nameof(hashedPassword));

            try
            {
                byte[] hashBytes = Convert.FromBase64String(hashedPassword);
                if (hashBytes.Length != SaltSize + HashSize)
                    return false;

                byte[] storedHash = new byte[HashSize];
                Array.Copy(hashBytes, 0, storedHash, 0, HashSize);

                byte[] salt = new byte[SaltSize];
                Array.Copy(hashBytes, HashSize, salt, 0, SaltSize);

                byte[] computedHash = HashPasswordWithSalt(password, salt);
                return Comparison(storedHash, computedHash);
            }
            catch 
            {
                return false;
            }
        }

        private static bool Comparison(byte[]a, byte[] b)
        {
            if (a.Length != b.Length) return false;

            int result = 0;
            for(int i = 0; i < a.Length; i++)
            {
                result |= a[i] ^ b[i];
            }
            return result == 0;
        }

        private static byte[] HashPasswordWithSalt(string password, byte[] salt)
        {
            return Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                Iterations,
                HashAlgorithmName.SHA256,
                HashSize
            );
        }
    }
}
