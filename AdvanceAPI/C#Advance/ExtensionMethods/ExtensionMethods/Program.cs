using System;
using System.Collections.Generic;
namespace ExtensionMethods
{
    /// <summary>
    /// Creates a sample dictionary with student names and displays the dictionary contents using an extended method.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // Create a sample dictionary
            Dictionary<int, string> student = new Dictionary<int, string>
            {
                { 1, "Nahi" },
                { 2, "Anjali" },
                { 3, "Hina" }
            };

            // Create an instance of DisplayDictionary from the DisplayDictionaryClassLibrary to use the extended method
            DisplayDictionary displayHelper = new DisplayDictionary();

            // Use the DisplayDictionaryMethod to display the contents of the dictionary
            displayHelper.DisplayDictionaryMethod(student);

            // Wait for the user to press any key before closing the console window
            Console.ReadKey();
        }
    }
}
