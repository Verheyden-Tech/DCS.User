using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DCS.User
{
    public class CryptographyHelper
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

        public static string HashSHA256(string toHash)
        {
            return HashSHA256(toHash, Encoding.UTF8);
        }

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
