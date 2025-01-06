using System;

namespace DebuggerTest
{
    public class Program
    {
        /// <summary>
        /// Entry point for the application. Simulates various debugging scenarios in a console application.
        /// </summary>
        static void Main(string[] args)
        {
            // Simple Breakpoint - Put a breakpoint on this line to pause and inspect the flow.
            int result = Sum(10, 20); // Example function where you can set a breakpoint
            Console.WriteLine("Result: " + result);

            // Simulating method that needs debugging
            CheckResults(result);

            // Example of logging with Tracepoint
            LogAction("Program execution started.");

            // Temporary breakpoint example
            TemporaryAction();

            // Hit count example inside loop
            LoopExample();

            Console.ReadLine();
        }

        /// <summary>
        /// Sums two integers and triggers a conditional breakpoint when the first integer is greater than 10.
        /// </summary>
        /// <param name="a">First integer</param>
        /// <param name="b">Second integer</param>
        /// <returns>Sum of a and b</returns>
        static int Sum(int a, int b)
        {
            // Conditional Breakpoint: Break when 'a' is greater than 10
            if (a > 10)
            {
                Console.WriteLine($"Conditional Breakpoint Hit: a={a}, b={b}");
            }
            return a + b;
        }

        /// <summary>
        /// Checks the result and breaks only if the result is equal to 30 (Dependent Breakpoint).
        /// </summary>
        /// <param name="result">The calculated result from the Sum method</param>
        static void CheckResults(int result)
        {
            // Dependent Breakpoint: Trigger only when 'result' is 30
            if (result == 30)
            {
                Console.WriteLine("Dependent Breakpoint Hit: Result equals 30");
            }
        }

        /// <summary>
        /// Logs actions to the output window using Tracepoint (logs without halting execution).
        /// </summary>
        /// <param name="action">Action description to log</param>
        static void LogAction(string action)
        {
            // Tracepoint: Output to debug window
            System.Diagnostics.Debug.WriteLine($"Tracepoint: {action}");
        }

        /// <summary>
        /// Demonstrates a Temporary Breakpoint that will hit only once.
        /// </summary>
        static void TemporaryAction()
        {
            // Temporary Breakpoint: Will be hit only once.
            Console.WriteLine("Temporary Breakpoint Hit: This should hit only once.");
        }

        /// <summary>
        /// A loop example that uses a hit count breakpoint to pause when the loop reaches a specific iteration (e.g., i == 5).
        /// </summary>
        static void LoopExample()
        {
            for (int i = 0; i < 10; i++)
            {
                // Set a Hit Count breakpoint at this point in the loop (e.g., at i == 5)
                if (i == 5) // Hit count set for i == 5
                {
                    // Inspect value of i when breakpoint hits
                    Console.WriteLine("Hit Count Breakpoint: i=" + i);
                }
            }
        }

        
    }
}
