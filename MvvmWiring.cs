using System.Diagnostics;

namespace MauiCommunityToolkitMvvmSample;

internal static class MvvmWiring
{
    public static IServiceCollection AutoRegister(this IServiceCollection services)
    {
        var stopwatch = Stopwatch.StartNew();

        var assembly = typeof(MvvmWiring).Assembly;

        if (assembly == null)
            throw new InvalidOperationException("No assemblies found in the current domain.");

        var typesToRegister = assembly.GetTypes()
            .Where(type => type.Name.EndsWith("Page", StringComparison.OrdinalIgnoreCase) ||
                           type.Name.EndsWith("View", StringComparison.OrdinalIgnoreCase) ||
                           type.Name.EndsWith("ViewModel", StringComparison.OrdinalIgnoreCase) ||
                           type.Name.EndsWith("Service", StringComparison.OrdinalIgnoreCase))
            .ToList();

        foreach (var type in typesToRegister)
        {
            Debug.WriteLine($"Register: {type}");
            services.AddSingleton(type);
        }

        stopwatch.Stop();
        Debug.WriteLine($"AutoRegister completed in {stopwatch.ElapsedMilliseconds} milliseconds.");

        return services;
    }
}
