namespace homework_dependancy_injection.di_core;

using enums;

public class ServiceProvider(IReadOnlyList<ServiceDescriptor> serviceDescriptors)
{
    private readonly Dictionary<Type, object> _singletonInstances = [];

    public T GetRequiredService<T>()
    {
        return (T)GetService(typeof(T));
    }

    public object GetService(Type serviceType)
    {
        var descriptor = serviceDescriptors.FirstOrDefault(x => x.ServiceType == serviceType) ??
                         throw new Exception($"Service of type {serviceType.Name} not registered");

        return descriptor.Lifetime switch
        {
            ServiceLifetime.Transient => CreateInstance(descriptor.ImplementationType),
            ServiceLifetime.Singleton => GetOrCreateSingleton(serviceType, descriptor),
            _ => throw new NotImplementedException(),
        };
    }

    private object GetOrCreateSingleton(Type serviceType, ServiceDescriptor descriptor)
    {
        if (_singletonInstances.TryGetValue(serviceType, out var existingInstance))
        {
            return existingInstance;
        }

        var createdInstance = CreateInstance(descriptor.ImplementationType);
        _singletonInstances[serviceType] = createdInstance;

        return createdInstance;
    }

    public object CreateInstance(Type implementationType)
    {
        var constructorInfo = implementationType.GetConstructors().First() ??
                              throw new Exception($"No public constructor found for type {implementationType.Name}");
        var parameters = constructorInfo.GetParameters()
            .Select(p => GetService(p.ParameterType))
            .ToArray();

        return Activator.CreateInstance(implementationType, parameters) ??
               throw new Exception($"Failed to create instance of type {implementationType.Name}");
    }
}