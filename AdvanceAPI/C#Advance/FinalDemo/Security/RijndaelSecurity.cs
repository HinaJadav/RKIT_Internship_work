using System;
using System.IO;
using System.Security.Cryptography;

namespace FinalDemo.Security
{
    /// <summary>
    /// Provides methods for encrypting and decrypting data using the Rijndael encryption algorithm 
    /// </summary>
    public static class RijndaelSecurity
    {
        private static readonly byte[] key;
        private static readonly byte[] iv;
        private static readonly int blockSize = 128;

        static RijndaelSecurity()
        {
            key = new byte[32];
            iv = new byte[16];

            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(key);
                rng.GetBytes(iv);
            }
        }

        /// <summary>
        /// Encrypts the given plain text using AES encryption.
        /// </summary>
        /// <param name="plainText">The plain text to encrypt.</param>
        /// <returns>Base64 encoded encrypted string.</returns>
        public static string Encrypt(string plainText)
        {
            using (RijndaelManaged rijndael = new RijndaelManaged())
            {
                rijndael.KeySize = 256;
                rijndael.BlockSize = blockSize;
                rijndael.Mode = CipherMode.CBC;
                rijndael.Padding = PaddingMode.PKCS7;
                rijndael.Key = key;
                rijndael.IV = iv;

                using (CreateEncryptor encryptor = rijndael.CreateEncryptor())
                using (MemoryStream ms = new MemoryStream())
                using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                using (StreamReader writer = new StreamWriter(cs))
                {
                    writer.Write(plainText);
                    writer.Flush();
                    cs.FlushFinalBlock();
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        /// <summary>
        /// Decrypts the given Base64 encoded ciphertext back to plain text.
        /// </summary>
        /// <param name="cipherText">The encrypted text (Base64 encoded).</param>
        /// <returns>Decrypted plain text.</returns>
        public static string Decrypt(string cipherText)
        {
            byte[] cipherBytes = Convert.FromBase64String(cipherText);

            using (RijndaelManaged rijndael = new RijndaelManaged())
            {
                rijndael.KeySize = 256;
                rijndael.BlockSize = blockSize;
                rijndael.Mode = CipherMode.CBC;
                rijndael.Padding = PaddingMode.PKCS7;
                rijndael.Key = key;
                rijndael.IV = iv;

                using (CreateDecryptor decryptor = rijndael.CreateDecryptor())
                using (MemoryStream ms = new MemoryStream(cipherBytes))
                using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                using (StreamReader reader = new StreamReader(cs))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}