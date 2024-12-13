using System;

namespace StringClass
{
    public class Program
    {
        /// <summary>
        /// The Main method demonstrates various string operations in C#.
        /// </summary>
        static void Main(string[] args)
        {
            /// <summary>
            /// User's name.
            /// </summary>
            string userName = "Priyank";
            Console.WriteLine($"User Name: {userName}");

            /// <summary>
            /// Description of the user.
            /// </summary>
            string userDescription = "He is a student in primary school";
            Console.WriteLine($"User Description: {userDescription}");

            /// <summary>
            /// Length of the user description string.
            /// </summary>
            int lengthOfUserDescription = userDescription.Length;
            Console.WriteLine($"Length of User Description: {lengthOfUserDescription}");

            /// <summary>
            /// User name in lowercase.
            /// </summary>
            string loginUserName = userName.ToLower();
            Console.WriteLine($"Login User Name (lowercase): {loginUserName}");

            /// <summary>
            /// User name in uppercase.
            /// </summary>
            string adminUserName = userName.ToUpper();
            Console.WriteLine($"Admin User Name (uppercase): {adminUserName}");

            /// <summary>
            /// User's full name after concatenation.
            /// </summary>
            string userSurname = "Jadav";
            string userFullName = userName + " " + userSurname;
            Console.WriteLine($"User Full Name: {userFullName}");

            /// <summary>
            /// New user name using string concatenation.
            /// </summary>
            string newUserName = string.Concat(userName, userSurname[0]);
            Console.WriteLine($"New User Name: {newUserName}");

            /// <summary>
            /// Position of a character in the string.
            /// </summary>
            int positionOfChar = newUserName.IndexOf("P");
            Console.WriteLine($"Position of character 'P': {positionOfChar}");

            /// <summary>
            /// Substring extracted from the new user name.
            /// </summary>
            string subStringUserName = newUserName.Substring(positionOfChar);
            Console.WriteLine($"Substring from position of 'P': {subStringUserName}");

            /// <summary>
            /// Welcome message for the user.
            /// </summary>
            string welcomeUserMessage = $"Welcome to the C# world, \t\"{userName}\"";
            Console.WriteLine(welcomeUserMessage);

            /// <summary>
            /// Updated description of the user after replacing a word.
            /// </summary>
            string replacedDescription = userDescription.Replace("primary", "secondary");
            Console.WriteLine($"Updated User Description: {replacedDescription}");

            /// <summary>
            /// Words extracted from the user description.
            /// </summary>
            string[] words = userDescription.Split(' ');
            Console.WriteLine("Words in User Description:");
            foreach (string word in words)
            {
                Console.WriteLine(word);
            }

            /// <summary>
            /// Trimmed string after removing leading and trailing spaces.
            /// </summary>
            string paddedString = "   C# Programming   ";
            string trimmedString = paddedString.Trim();
            Console.WriteLine($"Trimmed String: '{trimmedString}'");

            /// <summary>
            /// Check if the description contains a specific word.
            /// </summary>
            bool containsWord = userDescription.Contains("student");
            Console.WriteLine($"Description contains 'student': {containsWord}");

            /// <summary>
            /// Compare user name with a string (case insensitive).
            /// </summary>
            bool isEqual = userName.Equals("priyank", StringComparison.OrdinalIgnoreCase);
            Console.WriteLine($"User Name equals 'priyank' (case insensitive): {isEqual}");

            /// <summary>
            /// Check if the description starts or ends with specific words.
            /// </summary>
            bool startsWithHe = userDescription.StartsWith("He");
            bool endsWithSchool = userDescription.EndsWith("school");
            Console.WriteLine($"Description starts with 'He': {startsWithHe}");
            Console.WriteLine($"Description ends with 'school': {endsWithSchool}");
        }
    }
}
