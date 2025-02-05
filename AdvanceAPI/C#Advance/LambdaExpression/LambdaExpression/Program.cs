using System;
using System.Collections.Generic;
using System.Linq;

namespace LambdaExpression
{
    /// <summary>
    /// User class to represent user information.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the user's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the user's email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the user's password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Initializes a new instance of the User class.
        /// </summary>
        /// <param name="name">The user's name.</param>
        /// <param name="email">The user's email.</param>
        /// <param name="password">The user's password.</param>
        public User(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }
    }

    public class Program
    {
        /// <summary>
        /// The entry point of the program where user list is defined, and user search takes place.
        /// </summary>
        static void Main(string[] args)
        {
            /// <summary>
            /// List of users to store multiple user objects.
            /// </summary>
            List<User> users = new List<User>
            {
                new User("Priyank", "pu@gmail.com", "pu@123"),
                new User("Madhu", "madhu@gmail.com", "madhu@123"),
                new User("Suresh", "suresh@gmail.com", "suresh@123"),
                new User("Anjali", "anu@gmail.com", "anu@123")
            };

            /// <summary>
            /// Extract all current users' names using LINQ's Select method.
            /// </summary>
            List<string> AllUserNames = users.Select(x => x.Name).ToList();

            /// <summary>
            /// Display the list of all current users' names.
            /// </summary>
            Console.WriteLine("All current users' names: ");
            foreach (string userName in AllUserNames)
            {
                Console.WriteLine(userName); // Output each user's name
            }

            /// <summary>
            /// Prompt the user to input a name to search.
            /// </summary>
            Console.WriteLine("Enter user name whose information you want to fetch: ");
            string searchedUser = Console.ReadLine(); // Get the user input for search

            /// <summary>
            /// Search for the user using a lambda expression as a predicate.
            /// </summary>
            User searchedUserInfo = users.Where(u => u.Name == searchedUser).FirstOrDefault();


            /// <summary>
            /// Check if the user is found and display information.
            /// </summary>
            if (searchedUserInfo != null)
            {
                // Display the information of the searched user
                Console.WriteLine("\nUser information found: ");
                Console.WriteLine($"Name: {searchedUserInfo.Name}");
                Console.WriteLine($"Email: {searchedUserInfo.Email}");
                Console.WriteLine($"Password: {searchedUserInfo.Password}");
            }
            else
            {
                /// <summary>
                /// Inform the user if no matching user was found.
                /// </summary>
                Console.WriteLine("No user found with the given name.");
            }

            /// <summary>
            /// Sort users by the first letter of their name in descending order.
            /// </summary>
            List<User> sortedUsersByTheirName = users.OrderByDescending(user => user.Name[0]).ToList();

            /// Display the sorted list of users.
            /// <summary>
            /// </summary>
            Console.WriteLine("\nUsers sorted by the first letter of their name (descending):");
            foreach (User user in sortedUsersByTheirName)
            {
                Console.WriteLine($"Name: {user.Name}, Email: {user.Email}, Password: {user.Password}");
            }

            // A

            // use "new" keyword with lambda expression 
           
            // creating and initializing objects:
            // Using lambda expression with 'new' to create a new User object
            List<User> updatedUsers = users.Select(u => new User(u.Name.ToUpper(), u.Email, u.Password)).ToList();

            Console.WriteLine("Updated user names:");
            foreach (User user in updatedUsers)
            {
                Console.WriteLine($"Name: {user.Name}, Email: {user.Email}, Password: {user.Password}");
            }

            // Creating Collections of Objects
            //  create a dictionary using lambda expression --> in this we can use "new" to combine multiple fields 
            var userDictionary = users.ToDictionary(user1 => user1.Name, user1 => new { user1.Email, user1.Password });
            // return type
            Console.WriteLine("User Dictionary:");
            foreach (var user in userDictionary)
            {
                Console.WriteLine($"Name: {user.Key}, Email: {user.Value.Email}, Password: {user.Value.Password}");
            }

            
            Console.ReadKey();
        }
    }
}
