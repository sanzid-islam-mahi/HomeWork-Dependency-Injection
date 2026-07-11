namespace homework_dependancy_injection;

public class ServiceProvider
{
    private readonly IReadOnlyList<ServiceDescriptor> _serviceDescriptors;
    public ServiceProvider(IReadOnlyList<ServiceDescriptor> serviceDescriptors)
    {
        _serviceDescriptors = serviceDescriptors;
    }
    public T GetRequiredService<T>()
    {
        return (T)GetService(typeof(T));
    }
    public object GetService(Type serviceType)
    {
        var descriptor = _serviceDescriptors.FirstOrDefault(x => x.ServiceType == serviceType) ?? throw new Exception($"Service of type {serviceType.Name} not registered");

        return descriptor.Lifetime switch
        {
            ServiceLifetime.Transient => CreateInstance(descriptor.ImplementationType),
            _ => throw new NotImplementedException()
        };

    }
    public object CreateInstance(Type implementationType)
    {
        var constructorInfo = implementationType.GetConstructors()?.First() ?? throw new Exception($"No public constructor found for type {implementationType.Name}");
        var parameters = constructorInfo.GetParameters()
            .Select(p => GetService(p.ParameterType))
            .ToArray();

        return Activator.CreateInstance(implementationType, parameters) ?? throw new Exception($"Failed to create instance of type {implementationType.Name}");
    }
}