using System;

namespace ConditionalCompilation
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Application started.");

#if DEBUG
            Console.WriteLine("DEBUG mode: Detailed logging enabled.");
            DebugModeSpecificCode();
#endif

#if RELEASE
            Console.WriteLine("RELEASE mode: Optimized for production.");
#endif

#if FEATURE_X
            Console.WriteLine("Feature X is enabled.");
#else
            Console.WriteLine("Feature X is disabled.");
#endif

            Console.WriteLine("Application finished.");

            Console.ReadLine();
        }

        // Method specific to Debug mode
#if DEBUG
        static void DebugModeSpecificCode()
        {
            Console.WriteLine("Executing debug-specific code...");
        }
#endif

        
    }
}
