namespace homework_dependancy_injection.di_core;

using enums;

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

    public void AddScoped<TService, TImplementation>()
    {
        _serviceDescriptors.Add(new ServiceDescriptor(
            typeof(TService),
            typeof(TImplementation),
            ServiceLifetime.Scoped));
    }

    public ServiceProvider BuildServiceProvider()
    {
        return new ServiceProvider(_serviceDescriptors.AsReadOnly());
    }
}
