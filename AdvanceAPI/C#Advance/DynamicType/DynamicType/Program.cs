using System;

namespace DynamicType
{
    /// <summary>
    /// Represents a Person with an ID and Name.
    /// </summary>
    class Person
    {
        /// <summary>
        /// Gets or sets the ID of the person.
        /// </summary>
        public int PersonId { get; set; }

        /// <summary>
        /// Gets or sets the Name of the person.
        /// </summary>
        public string Name { get; set; }
    }

    public class Program
    {
        /// <summary>
        /// Entry point of the program. Demonstrates usage of dynamic and implicit types.
        /// </summary>
        static void Main(string[] args)
        {
            /// <summary>
            /// Creates an instance of the Person class and initializes its properties.
            /// </summary>
            Person person1 = new Person
            {
                PersonId = 1,
                Name = "Nahii"
            };

            /// <summary>
            /// Holds the name of the company as a dynamic variable.
            /// </summary>
            dynamic companyName = "ABC";
            Console.WriteLine($"Company Name: {companyName}");

            /// <summary>
            /// Converts the Person object (person1) to a dynamic type.
            /// </summary>
            dynamic dynamicPerson = person1;

            /// <summary>
            /// Retrieves the Name property from the dynamic Person object.
            /// </summary>
            dynamic Name = dynamicPerson.Name;

            /// <summary>
            /// Retrieves the PersonId property from the dynamic Person object.
            /// </summary>
            dynamic personId = dynamicPerson.PersonId;

            Console.WriteLine($"Dynamic Person Name: {Name}");
            Console.WriteLine($"Dynamic Person ID: {personId}");

            /// <summary>
            /// Converts the dynamic type back to a Person object.
            /// </summary>
            Person person2 = dynamicPerson;

            /// <summary>
            /// Retrieves the PersonId from the dynamic variable and assigns it to an integer.
            /// </summary>
            int person2Id = personId;

            /// <summary>
            /// Retrieves the Name from the dynamic variable and assigns it to a string.
            /// </summary>
            string person2Name = Name;

            Console.WriteLine($"Implicit Person2 Name: {person2Name}");
            Console.WriteLine($"Implicit Person2 ID: {person2Id}");

            Console.ReadKey();
        }
    }
}
