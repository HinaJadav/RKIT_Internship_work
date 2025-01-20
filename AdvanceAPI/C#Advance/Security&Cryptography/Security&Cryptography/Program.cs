using Security_Cryptography.BL;
using System.Security.Cryptography;

namespace Security_Cryptography
{
    public class Program
    {
        /// <summary>
        /// Main method to interact with the user, encrypt and decrypt the password using AES algorithm.
        /// </summary>
        static void Main(string[] args)
        {
            // Prompt the user to enter a username
            Console.Write("Please enter a username: ");
            string userName = Console.ReadLine() ?? string.Empty;  // Handle null input to avoid null reference exception

            // Prompt the user to enter a password
            Console.Write("Please enter a password: ");
            string password = Console.ReadLine() ?? string.Empty;  // Handle null input

            // Check if the password is empty, display a message and exit if it is
            if (string.IsNullOrEmpty(password))
            {
                Console.WriteLine("Password cannot be empty.");
                return;
            }

            // Generate a 16-byte key and IV (Initialization Vector) for AES encryption
            byte[] key = new byte[16];
            byte[] iv = new byte[16];

            // Use RandomNumberGenerator to securely generate the key and IV
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(key);  // Generate random key
                rng.GetBytes(iv);   // Generate random IV
            }

            // Encrypt the password using the AES algorithm
            byte[] encryptedPassword = AES.Encrypt(password, key, iv);
            string encryptedPasswordString = Convert.ToBase64String(encryptedPassword);  // Convert encrypted password to a base64 string for display
            Console.WriteLine("Encrypted password: " + encryptedPasswordString);

            // Decrypt the encrypted password back to its original form
            string decryptedPassword = AES.Decrypt(encryptedPassword, key, iv);
            Console.WriteLine("Decrypted password: " + decryptedPassword);

            // Wait for the user to press Enter before closing the application
            Console.ReadLine();
        }
    }
}
