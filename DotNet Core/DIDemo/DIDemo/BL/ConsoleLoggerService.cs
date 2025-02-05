namespace DIDemo.BL
{
    /// <summary>Logs messages to the console.</summary>
    public class ConsoleLoggerService : ILoggerService
    {
        /// <summary>Logs a message to the console.</summary>
        public void Log(string message)
        {
            Console.WriteLine($"Log: {message}");
        }
    }
}
