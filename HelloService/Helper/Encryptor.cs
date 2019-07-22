using System;
using System.Security.Cryptography;
using System.Text;

namespace HelloService.Helper
{
    public static class Encryptor
    {
        public static string EncryptSHA256(string text)
        {
            var sha1 = SHA256.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(text);
            byte[] outputBytes = sha1.ComputeHash(inputBytes);
            return BitConverter.ToString(outputBytes).Replace("-", "").ToLower();
        }
    }
}
