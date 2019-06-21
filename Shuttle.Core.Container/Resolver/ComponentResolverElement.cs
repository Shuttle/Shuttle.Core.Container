using System.Configuration;

namespace Shuttle.Core.Container
{
    public class ComponentResolverElement : ConfigurationElement
    {
        [ConfigurationProperty("dependencyType", IsRequired = true)]
        public string DependencyType => (string) this["dependencyType"];
    }
}