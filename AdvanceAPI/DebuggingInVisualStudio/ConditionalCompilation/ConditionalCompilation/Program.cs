using System;

namespace ConditionalCompilationExample
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Application started.");

            // Conditional compilation for Debug or Release mode
#if DEBUG
            Console.WriteLine("DEBUG Mode: Detailed logging enabled.");
            DebugModeSpecificCode();
#elif RELEASE
            Console.WriteLine("RELEASE Mode: Optimized for production.");
#endif

            // Feature toggle using custom symbols (FEATURE_X)
#if FEATURE_X
            Console.WriteLine("Feature X is enabled.");
#else
            Console.WriteLine("Feature X is disabled.");
#endif

            Console.WriteLine("Application finished.");
            Console.ReadLine();
        }

        // Debug-specific method
#if DEBUG
        static void DebugModeSpecificCode()
        {
            Console.WriteLine("Executing debug-specific code...");
        }
#endif
    }
}
