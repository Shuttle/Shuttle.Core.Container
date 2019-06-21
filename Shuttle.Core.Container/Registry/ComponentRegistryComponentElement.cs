using System;
using System.Configuration;

namespace Shuttle.Core.Container
{
    public class ComponentRegistryComponentElement : ConfigurationElement
    {
        [ConfigurationProperty("dependencyType", IsRequired = true)]
        public string DependencyType => (string) this["dependencyType"];

        [ConfigurationProperty("implementationType", IsRequired = false, DefaultValue = "")]
        public string ImplementationType => (string) this["implementationType"];

        [ConfigurationProperty("lifestyle", IsRequired = false, DefaultValue = "Singleton")]
        public Lifestyle Lifestyle => Enum.TryParse(this["lifestyle"].ToString(), true, out Lifestyle result) ? result : Lifestyle.Singleton;
    }
}