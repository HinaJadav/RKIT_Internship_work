using System;
using System.Collections.Generic;

namespace Generics
{
    /// <summary>
    /// A generic interface for displaying descriptions of items.
    /// </summary>
    /// <typeparam name="T">The type of description (e.g., string).</typeparam>
    public interface IItemDescription<T>
    {
        /// <summary>
        /// Displays the item description.
        /// </summary>
        /// <param name="description">The description of the item.</param>
        void DisplayItemDescription(T description);
    }

    /// <summary>
    /// Represents an item with a name and a price.
    /// </summary>
    public class Items : IItemDescription<string>
    {
        /// <summary>
        /// Gets or sets the name of the item.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the price of the item.
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Items"/> class with a specified name and price.
        /// </summary>
        /// <param name="Name">The name of the item.</param>
        /// <param name="Price">The price of the item.</param>
        public Items(string Name, int Price)
        {
            this.Name = Name;
            this.Price = Price;
        }

        /// <summary>
        /// Displays the name and price of the item.
        /// </summary>
        public void DisplayInformation()
        {
            Console.WriteLine($"Name: {Name}, Price: {Price}");
        }

        /// <summary>
        /// Displays a description of the item.
        /// </summary>
        /// <param name="description">The description of the item.</param>
        public void DisplayItemDescription(string description)
        {
            Console.WriteLine($"Description for {Name}: {description}");
        }
    }


    public class Program
    {
        /// <summary>
        /// A generic method that calculates and prints the total sum of integer values in a collection.
        /// </summary>
        /// <typeparam name="T">The type of collection that implements IEnumerable<int> (e.g., List<int>, int[]).</typeparam>
        /// <param name="value">The collection of integers to sum.</param>
        public static void PrintTotal<T>(T value) where T : IEnumerable<int>
        {
            int total = 0;

            // Iterate through each item in the collection and accumulate the sum
            foreach (int i in value)
            {
                total += i;
            }

            Console.WriteLine("Your total amount = " + total);
        }

       
        static void Main(string[] args)
        {
            /// <summary>
            /// Dictionary to store items where the key is an integer and the value is an Items object
            /// </summary>

            Dictionary<int, Items> itemsDictionary = new Dictionary<int, Items>();
           
            // Prompt the user to enter their daily expenses
            Console.WriteLine("Enter your daily expense amounts with item name to calculate the total:");

            // Prompt user on how to stop the input process
            Console.WriteLine("Type 'Close' when you want to stop entering amounts.");
            string input;
            string processStart = "Open";
            // Used as the key for the dictionary
            int index = 1; 

            // Loop to read expenses until 'Close' is entered
            while (processStart != "Close")
            {
                // Provide a message when the user is about to enter the item name
                Console.Write("Please enter the item name:");

                input = Console.ReadLine();

                // Check if the user wants to stop entering amounts
                if (input.Equals("Close", StringComparison.OrdinalIgnoreCase))
                {
                    processStart = "Close";
                }
                else
                {
                    // Ask for the price of the item
                    Console.Write("Enter price for " + input + ": ");
                    string priceInput = Console.ReadLine();

                    try
                    {
                       // Attempt to parse the price
                       int price = int.Parse(priceInput);

                        // Add the item to the dictionary with a unique index as the key
                        Items newItem = new Items(input, price);
                        itemsDictionary.Add(index, newItem);

                        // Use a description related to the item name instead of repeating price
                        newItem.DisplayItemDescription("This is a fresh and premium quality product.");

                        index++;
                    }
                    catch (FormatException)
                    {
                        // Handle invalid input by prompting the user for a valid number
                        Console.WriteLine("Invalid input! Please enter a valid integer or 'Close' to stop.");
                    }
                }
            }

            // Create a list of prices from the dictionary values
            List<int> prices = new List<int>();
            foreach (var item in itemsDictionary)
            {
                prices.Add(item.Value.Price);
                item.Value.DisplayInformation(); // Display each item information
            }

            // Use the generic PrintTotal method to display the sum of expenses
            PrintTotal(prices);

            // Wait for user input before closing the program
            Console.ReadLine();
        }
    }
}
