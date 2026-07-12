namespace homework_dependancy_injection.services;

using homework_dependancy_injection.interfaces;

public class EmailService : INotificationService
{
    public void Send(string message)
    {
        Console.WriteLine($"Email sent: {message}");
    }
}