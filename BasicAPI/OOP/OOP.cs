using System;

namespace OOP
{

    /// <summary>
    /// Represents an abstract class that defines common characteristics of a living being.
    /// 1) Abstraction
    /// </summary>
    public abstract class LivingBeing
    {
        // 2) Encapsulation
        // Scope: Instance
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        // Constructor to initialize the name of the living being
        protected LivingBeing(string name)
        {
            Name = name;
        }

        /// <summary>
        /// An abstract method that derived classes must implement to specify how the living being communicates.
        /// </summary>
        public abstract void Communicate();
    }

    /// <summary>
    /// Represents a Human being and implements the Communicate method.
    /// 3) Inheritance
    /// </summary>
    public class Human : LivingBeing
    {
        public Human(string name) : base(name)
        {
        }

        /// <summary>
        /// Implementation of the abstract Communicate method for the Human.
        /// </summary>
        public override void Communicate()
        {
            //Communicate();
            //base.Communicate()-->?
            Console.WriteLine($"{Name} is a human and can communicate verbally.");
        }
    }

    // Interface
    public interface IWorkable
    {
        void Work();
    }


    /// <summary>
    /// Represents an Employee, inheriting from Human.
    /// Inheritance and Polymorphism
    /// </summary>
    public class Employee : Human, IWorkable
    {
        public string JobTitle { get; set; }
        public int Salary { get; set; }

        public Employee(string name, string jobTitle, int salary) : base(name)
        {
            JobTitle = jobTitle;
            Salary = salary;
        }

        public void Work()
        {
            Console.WriteLine($"{Name} is working as a {JobTitle} and earning {Salary}.");
        }

        // polymorphism 
        public override void Communicate()
        {
            Console.WriteLine($"{Name} communicates as a professional in the workplace.");
        }
    }

    /// <summary>
    /// Represents a Man, demonstrating polymorphism with the Communicate method.
    /// </summary>
    public class Man : Human
    {
        // Scope: Static
        static double height = 6.1;
        public string Hobby { get; set; }

        public Man(string name, string hobby) : base(name)
        {
            Hobby = hobby;
        }

        /// <summary>
        /// Overrides Communicate to include hobby information.
        /// 4) Polymorphism
        /// </summary>
        public override void Communicate()
        {
            Console.WriteLine($"{Name} is a man and enjoys {Hobby}.");
        }

        // Static method
        public static void ManHeight()
        {
            Console.WriteLine($"The height of the man is {height} meters.");
        }
    }

    /// <summary>
    /// The entry point of the application to demonstrate OOP concepts.
    /// </summary>
    public class OOP
    {
        static void Main(string[] args)
        {
            // Scope: Local
            string welcomeMessage = "Hello User.";
            Console.WriteLine(welcomeMessage);

            // Encapsulation
            LivingBeing human = new Human("Priyank");
            human.Communicate();

            // Abstraction
            LivingBeing employee = new Employee("Suresh", "Software Developer", 85000);
            employee.Communicate();

            // Inheritance + polymorphism
            LivingBeing man = new Man("King", "playing football");
            man.Communicate();

            // Call the static method
            Man.ManHeight();

            // Interface
            IWorkable workableEmployee = new Employee("Ali", "Software Developer", 5000);
            workableEmployee.Work();

            Console.ReadLine();
        }
    }
}
