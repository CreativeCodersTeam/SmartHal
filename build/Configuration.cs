using System.ComponentModel;
using Nuke.Common.Tooling;

[TypeConverter(typeof(TypeConverter<Configuration>))]
#pragma warning disable CA1050 // Declare types in namespaces
public class Configuration : Enumeration
#pragma warning restore CA1050 // Declare types in namespaces
{
    public static readonly Configuration Debug = new() { Value = nameof(Debug) };
    
    public static readonly Configuration Release = new() { Value = nameof(Release) };

    public static implicit operator string(Configuration configuration)
    {
        return configuration.Value;
    }
}