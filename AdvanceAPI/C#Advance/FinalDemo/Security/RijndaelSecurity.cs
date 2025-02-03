using System;
using System.IO;
using System.Security.Cryptography;

namespace FinalDemo.Security
{
    /// <summary>
<<<<<<< HEAD
    /// Provides methods for encrypting and decrypting data using the Rijndael encryption algorithm.
=======
    /// Provides methods for encrypting and decrypting data using the Rijndael encryption algorithm (AES-256).
>>>>>>> 0f8054d594a105dfd50cdea410e51bb1e01a5a1a
    /// </summary>
    public static class RijndaelSecurity
    {
        // Define encryption settings (AES-256)
        private static readonly byte[] key;
        private static readonly byte[] iv;
        private static readonly int blockSize = 128; // AES uses a fixed 128-bit block size

        // Static constructor to generate key and IV only once
        static RijndaelSecurity()
        {
            key = new byte[32];  // 32 bytes = 256 bits (AES-256)
            iv = new byte[16];   // 16 bytes = 128 bits

            // Securely generate the key and IV
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
            using (var rijndael = new RijndaelManaged())
            {
                rijndael.KeySize = 256;
                rijndael.BlockSize = blockSize;
                rijndael.Mode = CipherMode.CBC;
                rijndael.Padding = PaddingMode.PKCS7;
                rijndael.Key = key;
                rijndael.IV = iv;

<<<<<<< HEAD
                using (var encryptor = rijndael.CreateEncryptor())
                using (var ms = new MemoryStream())
                using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                using (var writer = new StreamWriter(cs))
=======
                using (var encryptor = rijndael.CreateEncryptor()) // encryptor is the encryption logic  
                using (MemoryStream ms = new MemoryStream()) // it stored encrypted data into in-memory buffer(temporary container)
                using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write)) // apply the encryption to the data as it is written into the stream
                using (StreamWriter writer = new StreamWriter(cs)) // use to write PT
>>>>>>> 0f8054d594a105dfd50cdea410e51bb1e01a5a1a
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

            using (var rijndael = new RijndaelManaged())
            {
                rijndael.KeySize = 256;
                rijndael.BlockSize = blockSize;
                rijndael.Mode = CipherMode.CBC;
                rijndael.Padding = PaddingMode.PKCS7;
                rijndael.Key = key;
                rijndael.IV = iv;

<<<<<<< HEAD
                using (var decryptor = rijndael.CreateDecryptor())
                using (var ms = new MemoryStream(cipherBytes))
                using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                using (var reader = new StreamReader(cs))
=======
                using (var decryptor = rijndael.CreateDecryptor()) // creates Decryptor object 
                using (MemoryStream ms = new MemoryStream(cipherBytes)) // 
                using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                using (StreamReader reader = new StreamReader(cs))
>>>>>>> 0f8054d594a105dfd50cdea410e51bb1e01a5a1a
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
