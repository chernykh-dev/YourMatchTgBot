namespace YourMatchTgBot.ReflectionExtensions;

/// <summary>
/// Extentions for the factories
/// </summary>
public static class FactoryExtensions
{
    /// <summary>
    /// Adds the reflection factories
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddReflectionFactories(this IServiceCollection services)
    {
        // Must be AddScoped.
        services.
            AddSingleton<IDependencyReflectorFactory, DependencyReflectorFactory>();

        return services;
    }
}