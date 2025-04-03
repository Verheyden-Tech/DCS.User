using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DCS.User
{
    /// <summary>
    /// Provides methods for encrypting, decrypting, and hashing strings.
    /// </summary>
    public static class CryptographyHelper
    {
        private static byte[] EncryptString(byte[] clearText, byte[] Key, byte[] IV)
        {
            MemoryStream memoryStream = new MemoryStream();
            Aes aes = Aes.Create();
            aes.Key = Key;
            aes.IV = IV;
            CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(clearText, 0, clearText.Length);
            cryptoStream.Close();
            return memoryStream.ToArray();
        }

        /// <summary>
        /// Encrypts a string using AES encryption with a password.
        /// </summary>
        /// <param name="clearText">The string to be encrypted.</param>
        /// <param name="Password">The password used for encryption.</param>
        /// <returns>The encrypted string in Base64 format.</returns>
        public static string EncryptString(string clearText, string Password)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(clearText);
            PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes(Password, new byte[13]
            {
            73, 118, 97, 110, 32, 77, 101, 100, 118, 101,
            100, 101, 118
            });
            return Convert.ToBase64String(EncryptString(bytes, passwordDeriveBytes.GetBytes(32), passwordDeriveBytes.GetBytes(16)));
        }

        private static byte[] DecryptString(byte[] cipherData, byte[] Key, byte[] IV)
        {
            MemoryStream memoryStream = new MemoryStream();
            Aes aes = Aes.Create();
            aes.Key = Key;
            aes.IV = IV;
            CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(cipherData, 0, cipherData.Length);
            cryptoStream.Close();
            return memoryStream.ToArray();
        }

        /// <summary>
        /// Decrypts a string using AES decryption with a password.
        /// </summary>
        /// <param name="cipherText">The encrypted string in Base64 format.</param>
        /// <param name="Password">The password used for decryption.</param>
        /// <returns>The decrypted string.</returns>
        public static string DecryptString(string cipherText, string Password)
        {
            byte[] cipherData = Convert.FromBase64String(cipherText);
            PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes(Password, new byte[13]
            {
            73, 118, 97, 110, 32, 77, 101, 100, 118, 101,
            100, 101, 118
            });
            byte[] bytes = DecryptString(cipherData, passwordDeriveBytes.GetBytes(32), passwordDeriveBytes.GetBytes(16));
            return Encoding.Unicode.GetString(bytes);
        }

        /// <summary>
        /// Hashes a string using SHA256.
        /// </summary>
        /// <param name="toHash">raw password to hash.</param>
        /// <returns>SHA256 hashed string.</returns>
        public static string HashSHA256(string toHash)
        {
            return HashSHA256(toHash, Encoding.UTF8);
        }

        /// <summary>
        /// Hashes a string using SHA256 with the specified encoding.
        /// </summary>
        /// <param name="toHash">The string to hash.</param>
        /// <param name="encoding">The encoding to use.</param>
        /// <returns>The SHA256 hashed string.</returns>
        public static string HashSHA256(string toHash, Encoding encoding)
        {
            StringBuilder stringBuilder = new StringBuilder();
            using (SHA256 sHA = SHA256.Create())
            {
                byte[] array = sHA.ComputeHash(encoding.GetBytes(toHash));
                foreach (byte b in array)
                {
                    stringBuilder.Append(b.ToString("x2"));
                }
            }

            return stringBuilder.ToString();
        }

        public static string HashSHA256(byte[] toHash)
        {
            StringBuilder stringBuilder = new StringBuilder();
            using (SHA256 sHA = SHA256.Create())
            {
                byte[] array = sHA.ComputeHash(toHash);
                foreach (byte b in array)
                {
                    stringBuilder.Append(b.ToString("x2"));
                }
            }

            return stringBuilder.ToString();
        }

        public static string GetMD5Hash(string input)
        {
            return GetMD5Hash(input, Encoding.UTF8);
        }

        public static string GetMD5Hash(string input, Encoding encoding)
        {
            if (input == null || input.Length == 0)
            {
                return string.Empty;
            }

            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
            byte[] bytes = encoding.GetBytes(input);
            return BitConverter.ToString(mD5CryptoServiceProvider.ComputeHash(bytes));
        }
    }
}
