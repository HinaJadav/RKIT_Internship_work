using System;
using System.IO;

namespace ExceptionHandling
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Try/Catch: Handling divide by zero exception
            try
            {
                Console.Write("Enter Dividend: ");
                int divided = int.Parse(Console.ReadLine());

                Console.Write("Enter Divisor: ");
                int divisor = int.Parse(Console.ReadLine());

                int quotient = divided / divisor;
                Console.WriteLine($"Quotient: {quotient}");
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine("Error: Division by zero is not allowed.");
                Console.WriteLine($"Details: {ex.Message}");
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Error: Invalid input. Please enter numeric values.");
                Console.WriteLine($"Details: {ex.Message}");
            }

            // Try/Catch/Finally: Reading from an array
            try
            {
                int[] numbers = { 10, 20, 30 };
                Console.Write("Enter an index (0-2): ");
                int index = int.Parse(Console.ReadLine());
                Console.WriteLine($"Value at index {index}: {numbers[index]}");
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine("Error: Index out of range.");
                Console.WriteLine($"Details: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("This block executes regardless of exceptions.");
            }

            // Try/Finally: Ensuring resource cleanup
            System.IO.StreamReader reader = null;
            try
            {
                reader = new System.IO.StreamReader("example.txt");
                string content = reader.ReadToEnd();
                Console.WriteLine("File Content:");
                Console.WriteLine(content);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                    Console.WriteLine("File resource has been released.");
                }
            }

            // Handling multiple exceptions
            try
            {
                Console.Write("Enter numerator: ");
                int numerator = int.Parse(Console.ReadLine());

                Console.Write("Enter denominator: ");
                int denominator = int.Parse(Console.ReadLine());

                int result = numerator / denominator;
                Console.WriteLine($"Result: {result}");
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine("Error: Cannot divide by zero.");
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Error: Input must be a valid number.");
            }
            finally
            {
                Console.WriteLine("End of multiple exception handling example.");
            }

            // Throw exception
            try
            {
                var file = File.Open("file1.txt", FileMode.Open);
                file.Close();
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("File not found: " + ex.Message);

                // throw;
                throw ex;
                // throw new Exception("File was not found");
            }

            // Throw custom exception
            //Console.Write("Enter user's age: ");
            //int age = int.Parse(Console.ReadLine());

            //try
            //{
            //    validAgeForVote(age);
            //}
            //catch(InvalidAgeException ex)
            //{
            //    Console.WriteLine("ERROR: " + ex.massage();
            //}
        }

        //public static void validAgeForVote(int age)
        //{
        //    if(age < 18)
        //    {
        //        throw new
        //    }
        //}
    }
}
