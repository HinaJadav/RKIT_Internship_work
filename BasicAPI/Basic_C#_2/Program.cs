using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Basic_C__2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Keywords: var, const

            //const string studentName = "Priyank";
            //var std = 10;
            //var result = 96.70F;

            const string schoolName = "Alpha";

            //---------------------------------------------------------------------------------
            // Console Input/Output:

            Console.Write("Enter student name: ");
            var studentName = Console.ReadLine();

            Console.Write("Enter student's std: ");
            var studentStd = Console.ReadLine();

            Console.Write("Enter student's result: ");
            var studentResult = Console.ReadLine();

            Console.WriteLine("\nStudent Name: " + studentName);
            Console.WriteLine("Std: " + studentStd);
            Console.WriteLine("Result: " + studentResult);
            Console.WriteLine("School Name: " + schoolName);

            //-----------------------------------------------------------------------------------
            // Selection statements 

            // 1) if else 
            // Use: < <= > >=

            // Example 

            Console.Write("\nEnter your age: ");
            int age = int.Parse(Console.ReadLine());

            if (age < 10)
            {
                Console.WriteLine("You are a child.");
            }
            else if (age >= 10 && age < 18)
            {
                Console.WriteLine("You are a teenager.");
            }
            else if (age >= 18 && age < 60)
            {
                Console.WriteLine("You are an adult.");
            }
            else if (age >= 60)
            {
                Console.WriteLine("You are a senior citizen.");
            }
            else if(age < 0 || age > 150)
            {
                Console.WriteLine("Invalid age entered.");
            }


            //-----------------------------------------------------------------------------------

            // 2) switch

            Console.Write("\nEnter your score (0-100): ");
            int score = int.Parse(Console.ReadLine());

            // Calculate grade category
            switch (score / 10) // Divide the score by 10 to determine grade range
            {
                case 10: // For score = 100
                case 9:  // For scores 90-99
                    Console.WriteLine("Grade: A");
                    break;
                case 8:  // For scores 80-89
                    Console.WriteLine("Grade: B");
                    break;
                case 7:  // For scores 70-79
                    Console.WriteLine("Grade: C");
                    break;
                case 6:  // For scores 60-69
                    Console.WriteLine("Grade: D");
                    break;
                case 5:  // For scores 50-59
                case 4:  // For scores 40-49
                    Console.WriteLine("Grade: E");
                    break;
                default: // For scores below 40
                    Console.WriteLine("Grade: F");
                    break;
            }

            //-----------------------------------------------------------------------------------
            // Iteration Statements:

            // 1) for

            Console.WriteLine("\nLet's make your work easy!.");
            Console.Write("Enter your massage: ");
            string massage = Console.ReadLine();

            Console.Write("How many times do you want to repeat it?: ");
            int loopCounter = int.Parse(Console.ReadLine());

            if(loopCounter > 0)
            {
                for (int i = 0; i < loopCounter; i++)
                {
                    Console.WriteLine(massage);
                }
            }
            else
            {
                Console.WriteLine("Enter valid input for loop counter!");
            }

            // 2) while
            // Iteration happens until condition is true

            // Example : stone paper scissors 

            Console.WriteLine("\nLet's play Stone-Paper-Scissors! \nEnter 0 for stone, 1 for paper, 2 for scissors.");

            while (true)  // Infinite loop, will continue until player decides to exit
            {
                Console.WriteLine("player1 input: ");
                int player1 = int.Parse(Console.ReadLine());

                Console.WriteLine("player2 input: ");
                int player2 = int.Parse(Console.ReadLine());

                // Check the validity of inputs
                if (player1 < 0 || player1 > 2 || player2 < 0 || player2 > 2)
                {
                    Console.WriteLine("Invalid input! Please enter 0, 1, or 2.");
                    continue;  // Skip the rest of the loop and prompt again
                }

                // Determine the winner
                if (player1 == player2)
                {
                    Console.WriteLine("It's a tie!");
                }
                else if ((player1 == 0 && player2 == 2) || (player1 == 1 && player2 == 0) || (player1 == 2 && player2 == 1))
                {
                    Console.WriteLine("Player 1 wins!");
                }
                else
                {
                    Console.WriteLine("Player 2 wins!");
                }

                Console.WriteLine("Do you want to play again? (yes/no): ");
                string playAgain = Console.ReadLine().ToLower();

                if (playAgain != "yes")
                {
                    break;  // Exit the loop if player doesn't want to play again
                }
            }
        }
    }
}
