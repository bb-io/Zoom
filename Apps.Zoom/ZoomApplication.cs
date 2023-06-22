using Apps.Zoom.Connections.OAuth2;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication.OAuth2;

namespace Apps.Zoom;

public class ZoomApplication : IApplication
{
    public string Name
    {
        get => "Zoom";
        set { }
    }

    private readonly Dictionary<Type, object> _typesInstances;

    public ZoomApplication()
    {
        _typesInstances = CreateTypesInstances();
    }

    public T GetInstance<T>()
        => _typesInstances.TryGetValue(typeof(T), out var value)
            ? (T)value
            : throw new InvalidOperationException($"Instance of type '{typeof(T)}' not found");

    private Dictionary<Type, object> CreateTypesInstances()
        => new()
        {
            { typeof(IOAuth2AuthorizeService), new OAuth2AuthorizationSerivce() },
            { typeof(IOAuth2TokenService), new OAuth2TokenService() }
        };
}