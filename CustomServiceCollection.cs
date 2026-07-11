namespace homework_dependancy_injection;

public class CustomServiceCollection
{
    private readonly List<ServiceDescriptor> _serviceDescriptors = [];

    public void AddTransient<TService, TImplementation>()
    {
        _serviceDescriptors.Add(new ServiceDescriptor(
            typeof(TService),
            typeof(TImplementation),
            ServiceLifetime.Transient));

    }
    public void AddSingleton<TService, TImplementation>()
    {
        _serviceDescriptors.Add(new ServiceDescriptor(
            typeof(TService),
            typeof(TImplementation),
            ServiceLifetime.Singleton));
    }
    public ServiceProvider BuildServiceProvider()
    {
        return new ServiceProvider(_serviceDescriptors.AsReadOnly());
    }
}
