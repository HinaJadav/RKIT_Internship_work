using System;

namespace OOP
{
    /// <summary>
    /// Represents a person with basic details.
    /// </summary>
    public class Person
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the first name of the person.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the person.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the age of the person.
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Gets or sets the address of the person.
        /// </summary>
        public string Address { get; set; }

        #endregion

        #region Constructors

        // Default constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Person"/> class.
        /// </summary>
        public Person()
        {
        }

        // Parameterized constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Person"/> class with specified details.
        /// </summary>
        /// <param name="firstName">The first name of the person.</param>
        /// <param name="lastName">The last name of the person.</param>
        /// <param name="age">The age of the person.</param>
        /// <param name="address">The address (country, state, city) of the person.</param>
        public Person(string firstName, string lastName, int age, string address)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Address = address;
        }

        // copy constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Person"/> class by copying the properties from another instance 
        /// </summary>
        /// <param name="otherPerson">
        /// The <see cref="Person"/> instance to copy from.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="otherPerson"/> is null.
        /// </exception>
        public Person(Person otherPerson)
        {
            if(otherPerson == null)
            {
                throw new ArgumentNullException(nameof(otherPerson), "The provided person object cannot be null.");
            }

            FirstName = otherPerson.FirstName;
            LastName = otherPerson.LastName;
            Age = otherPerson.Age;
            Address = otherPerson.Address;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Displays a welcome message to the user.
        /// </summary>
        /// <param name="name">The full name of the person to welcome.</param>
        public void WelcomeMessage(string name)
        {
            Console.WriteLine($"Welcome, {name}, into the basic information form.\n");
        }

        #endregion
    }

    /// <summary>
    /// The main program class.
    /// </summary>
    public class BasicClass
    {
        /// <summary>
        /// The entry point of the application.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        static void Main(string[] args)
        {
            // Create a new person object with sample data.
            Person person = new Person("Priyank", "Jadav", 13, "India, Gujarat, Junagadh");

            // Combine first and last names for display.
            string personName = $"{person.FirstName} {person.LastName}";

            // Display the welcome message.
            person.WelcomeMessage(personName);

            // Print the person's details.
            Console.WriteLine($"Name: {personName}");
            Console.WriteLine($"Age: {person.Age}");
            Console.WriteLine($"Address: {person.Address}");

            // Keep the console open.
            Console.ReadLine();
        }
    }
}
