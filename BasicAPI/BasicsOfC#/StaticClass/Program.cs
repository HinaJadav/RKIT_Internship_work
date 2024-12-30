using System;

namespace StaticClassExample
{
    /// <summary>
    /// Provides utility methods for mathematical calculations.
    /// </summary>
    public static class MathUtilities
    {
        /// <summary>
        /// The constant value of Pi (approximately 3.14159).
        /// </summary>
        public static double Pi = 3.14159;

        /// <summary>
        /// Calculates the area of a circle given its radius.
        /// </summary>
        /// <param name="radius">The radius of the circle.</param>
        /// <returns>The area of the circle.</returns>
        public static double CalculateCircleArea(double radius)
        {
            return Pi * radius * radius;
        }
    }

    /// <summary>
    /// Demonstrates the use of the <see cref="MathUtilities"/> class.
    /// </summary>
    class Program
    {
        /// <summary>
        /// The entry point of the application.
        /// </summary>
        /// <param name="args">The command-line arguments.</param>
        static void Main(string[] args)
        {
            // Input: Radius of the circle
            double radius = 5.0;

            // Calculate the area using MathUtilities
            double area = MathUtilities.CalculateCircleArea(radius);

            // Output the result
            Console.WriteLine($"The area of a circle with radius {radius} is {area}");
        }
    }
}
