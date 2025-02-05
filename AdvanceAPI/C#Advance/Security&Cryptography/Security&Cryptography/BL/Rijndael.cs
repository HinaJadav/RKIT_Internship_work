using System.Security.Cryptography;

namespace Security_Cryptography.BL
{
    /// <summary>
    /// Provides methods for encrypting and decrypting data using the Rijndael encryption algorithm.
    /// </summary>
    public class Rijndael
    {
        /// <summary>
        /// Encrypts the given plain text using the Rijndael encryption algorithm.
        /// </summary>
        /// <param name="simpleText">The plain text to be encrypted.</param>
        /// <param name="key">The encryption key, which must be of a valid length for the Rijndael algorithm (e.g., 128, 192, or 256 bits).</param>
        /// <param name="iv">The initialization vector (IV) for the Rijndael algorithm, matching the block size divided by 8 (e.g., 16 bytes for 128-bit block size).</param>
        /// <param name="blockSize">The block size in bits for the Rijndael algorithm (e.g., 128, 192, or 256 bits).</param>
        /// <returns>A byte array containing the encrypted data (ciphertext).</returns>
        public static byte[] Encrypt(string simpleText, byte[] key, byte[] iv, int blockSize)
        {
            byte[] cipherText;

            // Initialize Rijndael encryption
            using (RijndaelManaged rijndael = new RijndaelManaged())
            {
                rijndael.KeySize = key.Length * 8; // Set encryption key size in bits
                rijndael.BlockSize = blockSize;    // Set block size(Amount of data the encryption algorithm process at a time) in bits
                rijndael.Key = key;                // Set encryption key
                rijndael.IV = iv;                  // Set initialization vector(Use for introduce randomness into encryption process)

                // Create an encryptor
                ICryptoTransform encryptor = rijndael.CreateEncryptor(rijndael.Key, rijndael.IV);

                // Encrypt the data
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                        {
                            // Write plain text to the CryptoStream to encrypt
                            streamWriter.Write(simpleText);
                        }

                        // Retrieve the encrypted data as a byte array
                        cipherText = memoryStream.ToArray();
                    }
                }
            }

            return cipherText;
        }

        /// <summary>
        /// Decrypts the given ciphertext back to its original plain text using the Rijndael encryption algorithm.
        /// </summary>
        /// <param name="cipherText">The encrypted byte array (ciphertext) to be decrypted.</param>
        /// <param name="key">The decryption key, which must match the key used for encryption.</param>
        /// <param name="iv">The initialization vector (IV) used during encryption, which must match the IV used for encryption.</param>
        /// <param name="blockSize">The block size in bits for the Rijndael algorithm (e.g., 128, 192, or 256 bits).</param>
        /// <returns>The decrypted plain text as a string.</returns>
        public static string Decrypt(byte[] cipherText, byte[] key, byte[] iv, int blockSize)
        {
            string plainText;

            // Initialize Rijndael decryption
            using (RijndaelManaged rijndael = new RijndaelManaged())
            {
                rijndael.KeySize = key.Length * 8; // Set key size in bits
                rijndael.BlockSize = blockSize;    // Set block size in bits
                rijndael.Key = key;                // Set decryption key
                rijndael.IV = iv;                  // Set initialization vector

                // Create a decryptor
                ICryptoTransform decryptor = rijndael.CreateDecryptor(rijndael.Key, rijndael.IV);

                // Decrypt the data
                using (MemoryStream memoryStream = new MemoryStream(cipherText))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader(cryptoStream))
                        {
                            // Read the decrypted data from the CryptoStream
                            plainText = streamReader.ReadToEnd();
                        }
                    }
                }
            }

            return plainText;
        }
    }
}
