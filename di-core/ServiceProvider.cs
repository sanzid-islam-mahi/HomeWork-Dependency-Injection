namespace homework_dependancy_injection.di_core;

using enums;

public class ServiceProvider(IReadOnlyList<ServiceDescriptor> serviceDescriptors)
{
    private readonly Dictionary<Type, object> _singletonInstances = [];

    public ServiceScope CreateScope()
    {
        return new ServiceScope(this);
    }

    
    public T GetRequiredService<T>()
    {
        return (T)GetService(typeof(T));
    }


    public object GetService(Type serviceType) => GetService(serviceType, null);
    internal object GetService(Type serviceType, Dictionary<Type,object>? scopedInstances)
    {
        var descriptor = serviceDescriptors.FirstOrDefault(x => x.ServiceType == serviceType) ??
                         throw new Exception($"Service of type {serviceType.Name} not registered");

        return descriptor.Lifetime switch
        {
            ServiceLifetime.Transient => CreateInstance(descriptor.ImplementationType),
            ServiceLifetime.Singleton => GetOrCreateSingleton(serviceType, descriptor.ImplementationType),
            ServiceLifetime.Scoped => GetOrCreateScoped(serviceType, descriptor.ImplementationType, scopedInstances),
            _ => throw new NotImplementedException(),
        };
    }

    private object GetOrCreateSingleton(Type serviceType, Type implementationType)
    {
        if (_singletonInstances.TryGetValue(serviceType, out var existingInstance))
        {
            return existingInstance;
        }

        var createdInstance = CreateInstance(implementationType);
        _singletonInstances[serviceType] = createdInstance;

        return createdInstance;
    }

    private object GetOrCreateScoped(Type serviceType, Type implementationType,
        Dictionary<Type, object>? scopedInstances)
    {
        if (scopedInstances == null)
        {
            throw new InvalidOperationException("Scoped instances can only be created within a scope.");
        }

        if (scopedInstances.TryGetValue(serviceType, out var existingInstance))
        {
            return existingInstance;
        }
        
        var createdInstance = CreateInstance(implementationType);
        scopedInstances[serviceType] = createdInstance;

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