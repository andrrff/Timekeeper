using Spectre.Console.Cli;

namespace Timekeeper.CLI.Services;

public sealed class TypeResolver : ITypeResolver, IDisposable
{
    private readonly IServiceProvider _provider;

    public TypeResolver(IServiceProvider provider)
    {
        _provider = provider ?? throw new ArgumentNullException(nameof(provider));
    }

    public object Resolve(Type? type)
    {
        if (type is null)
        {
            return default!;
        }

        return _provider.GetService(type) ?? new InvalidOperationException($"Could not resolve type '{type.FullName}'");
    }

    public void Dispose()
    {
        if (_provider is IDisposable disposable)
        {
            disposable.Dispose();
        }
    }
}