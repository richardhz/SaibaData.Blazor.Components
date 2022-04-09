using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaibaData.Blazor.Components.Services;

public record ResourceConfiguration(Type Type, string? AddPrefix = null);
public  class AssemblyResourceLocator
{
    private readonly Dictionary<string, string> resources;

    public string GetResource(string key) => resources[key];

    public AssemblyResourceLocator(params ResourceConfiguration[] configurations)
    {
        resources = new(StringComparer.OrdinalIgnoreCase);
        foreach (var configuration in configurations)
        {
            var assembly = configuration.Type.Assembly;

            var getResourceAsString = (string name) =>
            {
                using Stream? stream = assembly.GetManifestResourceStream(name);
                using var reader = new StreamReader(stream!);
                return reader.ReadToEnd().Trim();
            };

            var transformName = (string name) =>
            {
                var n1 = name.Replace($"{configuration.Type.Namespace}.", String.Empty);
                var n2 = (configuration.AddPrefix != null) ? $"{configuration.AddPrefix}.{n1}" : n1;
                var idx = n2.LastIndexOf('.');
                if (idx != -1)
                {
                    var n3 = n2[..idx].Replace(".", "/");  // upto and excluding  idx
                    var n4 = n2[idx..]; // from idx to end;
                    n2 = $"{n3}{n4}";
                }
                return n2;
            };

            //Resx resources show up with a .resources extension. We don't want them.
            var resourceNames = assembly.GetManifestResourceNames().Where(name => !name.EndsWith(".resources", StringComparison.OrdinalIgnoreCase));

            resourceNames.ToList().ForEach(name =>
            {
                var value = getResourceAsString(name);
                var key = transformName(name);
                resources[key] = value;
            });
        }
    }
}
