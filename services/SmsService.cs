namespace homework_dependancy_injection.services;

using homework_dependancy_injection.interfaces;

public class SmsService : INotificationService
{
    public void Send(string message)
    {
        Console.WriteLine($"SMS sent: {message}");
    }
}