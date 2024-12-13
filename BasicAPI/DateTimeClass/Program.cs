using System;

namespace DateTimeClass
{
    public class Program
    {
        static void Main(string[] args)
        {
            /// <summary>
            /// Holds the current local date and time.
            /// </summary>
            DateTime currentTime = DateTime.Now;

            /// <summary>
            /// Represents the minimum possible value for a DateTime.
            /// </summary>
            DateTime minValue = DateTime.MinValue;
            Console.WriteLine("Minimum Value : " + minValue);

            /// <summary>
            /// Represents the maximum possible value for a DateTime.
            /// </summary>
            DateTime maxValue = DateTime.MaxValue;
            Console.WriteLine("Maximum Value : " + maxValue);

            /// <summary>
            /// Holds the current UTC date and time.
            /// </summary>
            DateTime utcDateTime = DateTime.UtcNow;

            /// <summary>
            /// Retrieves the day of the week for the current time.
            /// </summary>
            string dayOfWeek = currentTime.DayOfWeek.ToString();
            Console.WriteLine("Day of the week: " + dayOfWeek);

            /// <summary>
            /// Retrieves the day of the year for the current time.
            /// </summary>
            int dayOfYear = currentTime.DayOfYear;
            Console.WriteLine("Day of the year: " + dayOfYear);

            // Demonstrating local to UTC and UTC to local conversions
            Console.WriteLine("\n--- DateTime Conversions ---");
            DateTime localToUtc = currentTime.ToUniversalTime();
            Console.WriteLine("Local to UTC: " + localToUtc);

            DateTime utcToLocal = utcDateTime.ToLocalTime();
            Console.WriteLine("UTC to Local: " + utcToLocal);

            // Demonstrating DateTime arithmetic
            Console.WriteLine("\n--- DateTime Arithmetic ---");
            Console.WriteLine("DateTime now : " + currentTime);
            Console.WriteLine("DateTime after adding 2 days: " + currentTime.AddDays(2));
            Console.WriteLine("DateTime after adding 3 hours : " + currentTime.AddHours(3));
            Console.WriteLine("DateTime after adding 30 minutes : " + currentTime.AddMinutes(30));
            Console.WriteLine("DateTime after adding 70 seconds : " + currentTime.AddSeconds(70));
            Console.WriteLine("DateTime after adding 1000 milliseconds: " + currentTime.AddMilliseconds(1000));
            Console.WriteLine("DateTime after adding 5 years : " + currentTime.AddYears(5));
            Console.WriteLine("DateTime after adding 3 months : " + currentTime.AddMonths(3));

            Console.WriteLine("DateTime before 2 days: " + currentTime.AddDays(-2));
            Console.WriteLine("DateTime before 3 hours : " + currentTime.AddHours(-3));
            Console.WriteLine("DateTime before 30 minutes : " + currentTime.AddMinutes(-30));
            Console.WriteLine("DateTime before 70 seconds : " + currentTime.AddSeconds(-70));
            Console.WriteLine("DateTime before 1000 milliseconds: " + currentTime.AddMilliseconds(-1000));

            // Calculating duration between two DateTime objects
            Console.WriteLine("\n--- Duration Calculation ---");
            TimeSpan duration = currentTime.Subtract(utcDateTime);
            Console.WriteLine("Time Difference: " + duration);
            Console.WriteLine("Day value of this duration: " + duration.Days);
            Console.WriteLine("Hour value of this duration: " + duration.Hours);
            Console.WriteLine("Minute value of this duration : " + duration.Minutes);
            Console.WriteLine("Second value of this duration : " + duration.Seconds);
            Console.WriteLine("Milliseconds value of this duration: " + duration.Milliseconds);

            // Parsing and converting DateTime to string
            Console.WriteLine("\n--- DateTime Parsing and Conversion ---");
            Console.WriteLine("Converted to string : " + currentTime.ToString());
        }
    }
}
