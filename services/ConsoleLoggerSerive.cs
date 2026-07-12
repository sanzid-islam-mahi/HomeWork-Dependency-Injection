using homework_dependancy_injection.interfaces;
namespace homework_dependancy_injection.services;


public class ConsoleLoggerService : ILogger
{
    public void Log(string message)
    {
        Console.WriteLine($"Log: {message}");
    }
}