using ClassTypes.Models;
using System;

namespace ClassTypes
{
    /// <summary>
    /// Main program for interacting with the User and Habit Tracker application.
    /// Allows the user to input personal, educational, and habit information, validate email,
    /// and calculate streaks for habits.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main entry point for the application. It orchestrates user interaction,
        /// including creating users, creating habits, validating email, and calculating streaks.
        /// </summary>
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the User and Habit Tracker Application!");

            // Create and display user information
            User user = CreateUser();
            user.DisplayInfo();

            // Create and display habit information
            Habit habit = CreateHabit();
            habit.DisplayInfo();

            // Calculate and display streak for a habit
            Console.WriteLine("\nCalculating streak...");
            int streak = Utility.CalculateStreak(habit.StartDate, habit.EndDate);
            Console.WriteLine($"The streak for the habit is {streak} days.");

            // Create a notification for the user after they set their habit
            Notification notification = new Notification
            {
                Message = $"Congratulations! You have successfully set up the habit '{habit.Name}'",
                CreatedAt = DateTime.Now,
                UserEmail = user.Email,
                IsRead = false
            };

            // Display the notification
            notification.DisplayNotification();

            Console.ReadLine();
        }

        /// <summary>
        /// Prompts the user for personal and educational information and creates a User object.
        /// Immediately validates the email when it is entered.
        /// </summary>
        /// <returns>A new User object populated with user input.</returns>
        static User CreateUser()
        {
            // Create a new User object to store user details
            User user = new User();

            Console.WriteLine("\nPlease enter your personal information:");

            // Prompt user for name and store it in the User object
            Console.Write("Name: ");
            user.Name = Console.ReadLine();

            // Prompt user for email and store it in the User object
            Console.Write("Email: ");
            user.Email = Console.ReadLine();

            // Validate the email as soon as it is entered
            if (!Utility.InValidEmail(user.Email))
            {
                Console.WriteLine("The email is invalid. Please enter a valid email.");
                // Re-prompt for a valid email
                Console.Write("Email: ");
                user.Email = Console.ReadLine();
            }

            // Prompt user for password and store it in the User object
            Console.Write("Password: ");
            user.Password = Console.ReadLine();

            // Prompt user for educational details and store them in the User object
            Console.Write("\nHighest Education: ");
            user.HighestEducation = Console.ReadLine();

            Console.Write("Institution Name: ");
            user.InstitutionName = Console.ReadLine();

            Console.Write("Field of Study: ");
            user.FieldOfStudy = Console.ReadLine();

            Console.WriteLine("\nPlease enter your address details:");

            user.Address = new User.AddressDetails();  

            Console.Write("Street: ");
            user.Address.Street = Console.ReadLine();

            Console.Write("City: ");
            user.Address.City = Console.ReadLine();

            Console.Write("State: ");
            user.Address.State = Console.ReadLine();

            Console.Write("Postal Code: ");
            user.Address.PostalCode = Console.ReadLine();

            return user;
        }

        /// <summary>
        /// Prompts the user for habit details and creates a Habit object.
        /// </summary>
        /// <returns>A new Habit object populated with habit input.</returns>
        static Habit CreateHabit()
        {
            // Create a new Habit object to store habit details
            Habit habit = new Habit();

            Console.WriteLine("\nPlease enter your habit details:");

            // Prompt user for the habit's name and store it in the Habit object
            Console.Write("Habit Name: ");
            habit.Name = Console.ReadLine();

            // Prompt user for the habit's category and store it in the Habit object
            Console.Write("Category (e.g., Health, Education, Fitness): ");
            habit.Category = Console.ReadLine();

            // Prompt user for the habit's frequency and store it in the Habit object
            Console.Write("Frequency (times per week): ");
            habit.Frequency = int.Parse(Console.ReadLine());

            // Prompt user for the start date of the habit and store it in the Habit object
            Console.Write("Start Date (yyyy-mm-dd): ");
            habit.StartDate = DateTime.Parse(Console.ReadLine());

            // Prompt user for the end date of the habit and store it in the Habit object
            Console.Write("End Date (yyyy-mm-dd): ");
            habit.EndDate = DateTime.Parse(Console.ReadLine());

            return habit;
        }
    }
}
