using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace OOP
{
    /// <summary>
    /// This program demonstrates the use of different types of collections in C#:
    /// 1. Non-Generic Collections:
    ///    `ArrayList`: A dynamic array capable of storing elements of any type.
    ///    `Hashtable`: A collection of key-value pairs, where keys and values can be of any type.
    /// 2. Generic Collections:
    ///    `List<T>`: A strongly-typed dynamic array.
    ///    `Dictionary<TKey, TValue>`: A collection of strongly-typed key-value pairs.
    /// 3. Specialized Collections:
    ///    `StringCollection`: A collection specifically designed for storing strings.
    ///    `Queue<T>` and `Stack<T>`: Examples of specialized generic collections.
    /// 4. Nested Collections:
    ///    `List<Dictionary<TKey, TValue>>` and `Dictionary<TKey, List<T>>` examples.
    /// </summary>
    public class Collection
    {
        /// <summary>
        /// 1. Simple Enum: `DaysOfWeek` represents the days of the week.
        /// 2. Flags Enum: `FileAccess` uses the `[Flags]` attribute to allow bitwise combinations of values.
        /// 3. Enum with Specific Values: `ErrorCode` maps HTTP status codes to meaningful names.
        /// </summary>

        // Represents the days of the week
        enum GameLevel
        {
            Easy,
            Medium,
            Hard,
            Expert
        }

        // Represents file access permissions with bitwise support
        [Flags]
        enum FileAccess
        {
            None = 0,
            Read = 1,
            Write = 2,
            Execute = 4
        }

        // Maps HTTP error codes to descriptive names
        enum ErrorCode
        {
            NotFound = 404,
            Unauthorized = 401,
            InternalServerError = 500
        }

        static void Main(string[] args)
        {
            // Non-Generic Collections
            // ArrayList 
            ArrayList arrayList = new ArrayList();
            arrayList.Add(1);
            arrayList.Add("Hello");
            arrayList.Add(3.14);

            foreach (var item in arrayList)
            {
                Console.WriteLine(item);
            }

            // Hashtable 
            Hashtable hashtable = new Hashtable();
            hashtable["Name"] = "Anjali";
            hashtable["Age"] = 25;

            foreach (DictionaryEntry entry in hashtable)
            {
                Console.WriteLine($"{entry.Key}: {entry.Value}");
            }

            // Generic Collections
            // List<T> 
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
            numbers.Add(6);

            foreach (var number in numbers)
            {
                Console.WriteLine(number);
            }

            // Dictionary<TKey, TValue> 
            Dictionary<string, string> capitals = new Dictionary<string, string>
            {
                { "India", "New Delhi" },
                { "USA", "Washington, D.C." }
            };

            foreach (var capital in capitals)
            {
                Console.WriteLine($"{capital.Key}: {capital.Value}");
            }

            // Specialized Collections
            // StringCollection 
            StringCollection stringCollection = new StringCollection();
            stringCollection.Add("Apple");
            stringCollection.Add("Banana");

            foreach (string item in stringCollection)
            {
                Console.WriteLine(item);
            }

            // Queue<T>
            Queue<string> queue = new Queue<string>();
            queue.Enqueue("First");
            queue.Enqueue("Second");
            queue.Enqueue("Third");

            Console.WriteLine("Queue elements:");
            while (queue.Count > 0)
            {
                Console.WriteLine(queue.Dequeue());
            }

            // Stack<T>
            Stack<int> stack = new Stack<int>();
            stack.Push(10);
            stack.Push(20);
            stack.Push(30);

            Console.WriteLine("Stack elements:");
            while (stack.Count > 0)
            {
                Console.WriteLine(stack.Pop());
            }

            // Using a simple enum
            GameLevel currentLevel = GameLevel.Easy;
            Console.WriteLine($"\nCurrent Level is: {currentLevel}");

            // Using a Flags enum with bitwise operations
            FileAccess access = FileAccess.Read | FileAccess.Write;
            Console.WriteLine($"File access permissions: {access}");

            // Nested Collections: List of Dictionaries
            List<Dictionary<string, int>> listOfDictionaries = new List<Dictionary<string, int>>
            {
                new Dictionary<string, int> { { "One", 1 }, { "Two", 2 } },
                new Dictionary<string, int> { { "Three", 3 }, { "Four", 4 } }
            };

            Console.WriteLine("List of Dictionaries:");
            foreach (var dict in listOfDictionaries)
            {
                foreach (var pair in dict)
                {
                    Console.WriteLine($"{pair.Key}: {pair.Value}");
                }
            }

            // Nested Collections: Dictionary of Lists
            Dictionary<string, List<string>> dictionaryOfLists = new Dictionary<string, List<string>>
            {
                { "Fruits", new List<string> { "Apple", "Banana" } },
                { "Vegetables", new List<string> { "Carrot", "Broccoli" } }
            };

            Console.WriteLine("Dictionary of Lists:");
            foreach (var kvp in dictionaryOfLists)
            {
                Console.WriteLine($"{kvp.Key}:");
                foreach (var value in kvp.Value)
                {
                    Console.WriteLine($"- {value}");
                }
            }

            Console.ReadLine();
        }
    }
}
