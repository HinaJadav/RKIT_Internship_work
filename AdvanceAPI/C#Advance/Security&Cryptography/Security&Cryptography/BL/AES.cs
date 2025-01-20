using System.Security.Cryptography;

namespace Security_Cryptography.BL
{
    /// <summary>
    /// A class that provides methods for AES encryption and decryption.
    /// </summary>
    public class AES
    {
        /// <summary>
        /// Encrypts the given plain text using AES algorithm with the provided key and IV (Initialization Vector).
        /// </summary>
        /// <param name="simpleText">The plain text to encrypt.</param>
        /// <param name="key">The encryption key used by AES algorithm.</param>
        /// <param name="iv">The initialization vector (IV) used for AES encryption.</param>
        /// <returns>A byte array containing the encrypted text (ciphertext).</returns>
        public static byte[] Encrypt(string simpleText, byte[] key, byte[] iv)
        {
            byte[] cipherText;

            // Create an instance of the AES encryption algorithm
            using (Aes aes = Aes.Create())
            {
                // Create an encryptor using the key and IV
                ICryptoTransform encryptor = aes.CreateEncryptor(key, iv);

                // Use a memory stream to hold the encrypted data
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    // Use a CryptoStream to perform encryption on the memory stream
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        // Write the plain text to the CryptoStream, which encrypts it
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(simpleText);
                        }

                        // Get the encrypted data as a byte array
                        cipherText = memoryStream.ToArray();
                    }
                }
            }

            // Return the encrypted byte array (ciphertext)
            return cipherText;
        }

        /// <summary>
        /// Decrypts the given encrypted byte array (ciphertext) back to the original plain text using AES algorithm with the provided key and IV.
        /// </summary>
        /// <param name="cipherText">The encrypted byte array to decrypt.</param>
        /// <param name="key">The decryption key used by AES algorithm.</param>
        /// <param name="iv">The initialization vector (IV) used for AES decryption.</param>
        /// <returns>The decrypted plain text as a string.</returns>
        public static string Decrypt(byte[] cipherText, byte[] key, byte[] iv)
        {
            string simpleText = String.Empty;

            // Create an instance of the AES encryption algorithm
            using (Aes aes = Aes.Create())
            {
                // Create a decryptor using the key and IV
                ICryptoTransform decryptor = aes.CreateDecryptor(key, iv);

                // Use a memory stream to hold the encrypted data
                using (MemoryStream memoryStream = new MemoryStream(cipherText))
                {
                    // Use a CryptoStream to perform decryption on the memory stream
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        // Read the decrypted data from the CryptoStream
                        using (StreamReader streamReader = new StreamReader(cryptoStream))
                        {
                            // Get the decrypted plain text as a string
                            simpleText = streamReader.ReadToEnd();
                        }
                    }
                }
            }

            // Return the decrypted plain text
            return simpleText;
        }
    }
}
