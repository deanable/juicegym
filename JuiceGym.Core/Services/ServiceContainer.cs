using JuiceGym.Core.Interfaces;

namespace JuiceGym.Core.Services;

/// <summary>
/// Simple service container for dependency injection
/// </summary>
public class ServiceContainer
{
    private readonly Dictionary<Type, Func<object>> _registrations = new();

    public void Register<TInterface, TImplementation>()
        where TImplementation : TInterface, new()
    {
        _registrations[typeof(TInterface)] = () => new TImplementation();
    }

    public void Register<TInterface>(Func<TInterface> factory)
    {
        _registrations[typeof(TInterface)] = () => factory();
    }

    public TInterface GetService<TInterface>()
    {
        if (_registrations.TryGetValue(typeof(TInterface), out var factory))
        {
            return (TInterface)factory();
        }

        throw new InvalidOperationException($"Service {typeof(TInterface).Name} not registered");
    }

    public static ServiceContainer CreateDefault()
    {
        var container = new ServiceContainer();

        // Register default implementations
        container.Register<IConfigurationService, ConfigurationService>();

        return container;
    }
}