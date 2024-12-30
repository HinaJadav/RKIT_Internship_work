using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Functions also called Methods
namespace Functions
{
    internal class Program
    {
        // Void Function
        public static void DisplayWelcomeMessage()
        {
            Console.WriteLine("Welcome to the User Information Program!");
        }

        // Return Type Function
        public static string GetFullName(string firstName, string lastName)
        {
            return $"{firstName} {lastName}";
        }

        // Optional Parameters
        // Parameters with default values are called optional parameters
        public static void DisplayBasicInfo(string name, int age = 18)
        {
            Console.WriteLine($"Name: {name}, Age: {age}");
        }

        // Out Parameters
        // Used to return multiple values from a method.
        public static void GetUserLocation(out string city, out string country)
        {
            Console.Write("Enter your city: ");
            city = Console.ReadLine();
            Console.Write("Enter your country: ");
            country = Console.ReadLine();
        }

        // Reference Parameters
        public static void IncreaseAge(ref int age)
        {
            age += 1; // Increment age by 1
        }

        static void Main()
        {
            // Call Void Function
            DisplayWelcomeMessage();

            // Collect User's First and Last Name
            Console.Write("Enter your first name: ");
            string firstName = Console.ReadLine();
            Console.Write("Enter your last name: ");
            string lastName = Console.ReadLine();

            // Call Return Type Function
            string fullName = GetFullName(firstName, lastName);

            // Collect Age with Default Handling
            Console.Write("Enter your age: ");
            int age = int.Parse(Console.ReadLine());

            // Modify Age Using Reference Parameter
            IncreaseAge(ref age);

            // Collect Location Using Out Parameters
            GetUserLocation(out string city, out string country);

            // Display Collected Information Using Optional Parameters
            DisplayBasicInfo(fullName, age);

            // Display Location Using Named Parameters
            // Named parameters allow you to specify parameter names when calling a method
            Console.WriteLine($"Location: City - {city}, Country - {country}");

            Console.ReadLine();
        }
    }
}
