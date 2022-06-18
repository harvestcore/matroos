using System.Reflection;

namespace Matroos.Resources.Extensions;

public class AssemblyInfo
{
    /// <summary>
    /// Assembly version.
    /// </summary>
    public string Version { get; }

    /// <summary>
    /// Assembly product.
    /// </summary>
    public string Product { get; }

    public AssemblyInfo(string version, string product)
    {
        Version = version;
        Product = product;
    }
}

public static class AssemblyExtensions
{
    public static AssemblyInfo GetAssemblyInfo()
    {
        return new(
            version: Assembly.GetEntryAssembly()?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? "0.0.0",
            product: Assembly.GetEntryAssembly()?.GetCustomAttribute<AssemblyProductAttribute>()?.Product ?? "Undefined"
        );
    }
}
