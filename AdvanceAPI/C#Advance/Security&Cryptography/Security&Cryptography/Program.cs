using Security_Cryptography.BL;
using System.Security.Cryptography;
using Rijndael = Security_Cryptography.BL.Rijndael;

namespace Security_Cryptography
{
    public class Program
    {
        /// <summary>
        /// Main method to interact with the user, encrypt and decrypt the password using AES or Rijndael algorithm.
        /// </summary>
        static void Main(string[] args)
        {
            // Prompt the user to enter a username
            Console.Write("Please enter a username: ");
            string userName = Console.ReadLine() ?? string.Empty;

            // Prompt the user to enter a password
            Console.Write("Please enter a password: ");
            string password = Console.ReadLine() ?? string.Empty;

            // Check if the password is empty, display a message and exit if it is
            if (string.IsNullOrEmpty(password))
            {
                Console.WriteLine("Password cannot be empty.");
                return;
            }

            // Prompt the user to select encryption method
            Console.WriteLine("Select encryption algorithm:");
            Console.WriteLine("1. AES");
            Console.WriteLine("2. Rijndael");
            Console.Write("Enter your choice (1 or 2): ");
            int choice;

            try
            {
                choice = int.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
                return;
            }

            // Validate user input
            if (choice != 1 && choice != 2)
            {
                Console.WriteLine("Invalid choice input.");
                return;
            }

            // Generate a 16-byte key and IV (Initialization Vector) for encryption
            byte[] key = new byte[16];
            byte[] iv = new byte[16];

            // Use RandomNumberGenerator to securely generate the key and IV
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(key);  // Generate random key
                rng.GetBytes(iv);   // Generate random IV
            }

            // Variables to store encrypted and decrypted passwords
            byte[] encryptedPassword = null;
            string decryptedPassword = string.Empty;

            // Execute encryption and decryption based on user choice
            switch (choice)
            {
                case 1: // AES
                    encryptedPassword = AES.Encrypt(password, key, iv);
                    decryptedPassword = AES.Decrypt(encryptedPassword, key, iv);
                    Console.WriteLine("\n--- Using AES Algorithm ---");
                    break;

                case 2: // Rijndael
                    int blockSize = 128; // Default block size
                    encryptedPassword = Rijndael.Encrypt(password, key, iv, blockSize);
                    decryptedPassword = Rijndael.Decrypt(encryptedPassword, key, iv, blockSize);
                    Console.WriteLine("\n--- Using Rijndael Algorithm ---");
                    break;
            }

            // Display encrypted and decrypted passwords
            string encryptedPasswordString = Convert.ToBase64String(encryptedPassword);  // Convert encrypted password to a base64 string for display
            Console.WriteLine("Encrypted password: " + encryptedPasswordString);
            Console.WriteLine("Decrypted password: " + decryptedPassword);

            // Wait for the user to press Enter before closing the application
            Console.ReadLine();
        }
    }
}
