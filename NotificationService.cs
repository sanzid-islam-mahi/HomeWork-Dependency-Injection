namespace homework_dependancy_injection;

public interface INotificationService
{
    void Send(string message);
}


public class EmailNotificationService : INotificationService
{
    public void Send(string message)
    {
        Console.WriteLine($"Sending email notification: {message}");
    }
}

public class SmsNotificationService : INotificationService
{
    public void Send(string message)
    {
        Console.WriteLine($"Sending SMS notification: {message}");
    }
}

public class NotificationService(EmailNotificationService notificationService)
{
    public void Notify(string message)
    {
        notificationService.Send(message);
    }
}