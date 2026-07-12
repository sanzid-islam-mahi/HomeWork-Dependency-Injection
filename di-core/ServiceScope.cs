namespace homework_dependancy_injection.di_core;

public class ServiceScope(ServiceProvider serviceProvider) : IDisposable
{
    private readonly Dictionary<Type, object> _scopedInstances= new();

    public T GetRequiredService<T>()
    {
        return (T)serviceProvider.GetService(typeof(T),_scopedInstances);
    }

    public void Dispose()
    {
        foreach (var instance in _scopedInstances.Values)
        {
            if (instance is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
        _scopedInstances.Clear();
    }
}