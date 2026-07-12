namespace homework_dependancy_injection.services;

interface ITransientService
{
    void PerformAction();
}
interface IScopedService
{
    void PerformAction();
}
interface ISingletonService
{
    void PerformAction();
}

public class TestServices : ITransientService, IScopedService, ISingletonService
{
    private readonly Guid _id;

    public TestServices()
    {
        _id = Guid.NewGuid();
    }

    public void PerformAction()
    {
        Console.WriteLine($"Service ID: {_id}");
    }
}