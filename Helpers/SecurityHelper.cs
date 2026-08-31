using System;
using System.Security.Cryptography;
using System.Text;

namespace POS.Helpers
{
    /// <summary>
    /// Cryptographic helper providing PBKDF2 password hashing (RFC 2898)
    /// with salt, iteration count, constant-time verification, and legacy SHA-256 migration support.
    /// </summary>
    public static class SecurityHelper
    {
        private const int SaltByteSize = 16; // 128-bit salt
        private const int HashByteSize = 32; // 256-bit subkey
        private const int Pbkdf2Iterations = 10000;
        private const string Pbkdf2Prefix = "PBKDF2$";

        /// <summary>
        /// Hashes a plain-text password using PBKDF2 with a cryptographically secure random salt.
        /// Format: PBKDF2${iterations}${salt_hex}${hash_hex}
        /// </summary>
        public static string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                return string.Empty;

            byte[] salt = new byte[SaltByteSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            byte[] hash;
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Pbkdf2Iterations))
            {
                hash = pbkdf2.GetBytes(HashByteSize);
            }

            string saltHex = ToHexString(salt);
            string hashHex = ToHexString(hash);

            return $"{Pbkdf2Prefix}{Pbkdf2Iterations}${saltHex}${hashHex}";
        }

        /// <summary>
        /// Verifies a plain-text password against a stored password hash (PBKDF2 or legacy SHA-256).
        /// Sets needsUpgrade to true if the stored hash was legacy SHA-256 or used fewer iterations.
        /// </summary>
        public static bool VerifyPassword(string password, string storedHash, out bool needsUpgrade)
        {
            needsUpgrade = false;

            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(storedHash))
                return false;

            // 1. Check if stored hash is PBKDF2 format
            if (storedHash.StartsWith(Pbkdf2Prefix, StringComparison.OrdinalIgnoreCase))
            {
                string[] parts = storedHash.Split('$');
                if (parts.Length == 4)
                {
                    if (int.TryParse(parts[1], out int iterations) && iterations > 0)
                    {
                        byte[] salt = FromHexString(parts[2]);
                        byte[] expectedHash = FromHexString(parts[3]);

                        if (salt != null && expectedHash != null)
                        {
                            byte[] actualHash;
                            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations))
                            {
                                actualHash = pbkdf2.GetBytes(expectedHash.Length);
                            }

                            bool match = FixedTimeEquals(actualHash, expectedHash);
                            if (match && iterations < Pbkdf2Iterations)
                            {
                                needsUpgrade = true;
                            }
                            return match;
                        }
                    }
                }
                return false;
            }

            // 2. Legacy SHA-256 check (64 hex characters)
            string legacyHash = ComputeSha256Hash(password);
            if (FixedTimeStringEquals(legacyHash, storedHash))
            {
                needsUpgrade = true; // Flag for automatic upgrade to PBKDF2
                return true;
            }

            return false;
        }

        /// <summary>
        /// Legacy SHA-256 hashing maintained for backward compatibility.
        /// </summary>
        public static string ComputeSha256Hash(string rawData)
        {
            if (string.IsNullOrEmpty(rawData)) return string.Empty;

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                return ToHexString(bytes);
            }
        }

        /// <summary>
        /// Constant-time byte array comparison to mitigate timing side-channel attacks.
        /// </summary>
        private static bool FixedTimeEquals(byte[] a, byte[] b)
        {
            if (a == null || b == null || a.Length != b.Length)
                return false;

            int diff = 0;
            for (int i = 0; i < a.Length; i++)
            {
                diff |= a[i] ^ b[i];
            }
            return diff == 0;
        }

        /// <summary>
        /// Constant-time string comparison.
        /// </summary>
        private static bool FixedTimeStringEquals(string a, string b)
        {
            if (a == null || b == null || a.Length != b.Length)
                return false;

            int diff = 0;
            for (int i = 0; i < a.Length; i++)
            {
                diff |= a[i] ^ b[i];
            }
            return diff == 0;
        }

        private static string ToHexString(byte[] bytes)
        {
            if (bytes == null) return string.Empty;
            StringBuilder sb = new StringBuilder(bytes.Length * 2);
            for (int i = 0; i < bytes.Length; i++)
            {
                sb.Append(bytes[i].ToString("x2"));
            }
            return sb.ToString();
        }

        private static byte[] FromHexString(string hex)
        {
            if (string.IsNullOrEmpty(hex) || hex.Length % 2 != 0)
                return null;

            try
            {
                byte[] bytes = new byte[hex.Length / 2];
                for (int i = 0; i < bytes.Length; i++)
                {
                    bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
                }
                return bytes;
            }
            catch
            {
                return null;
            }
        }
    }
}
