using System;
using System.Configuration;

namespace Shuttle.Core.Container
{
    public class ComponentRegistryCollectionElement : ConfigurationElementCollection
    {
        [ConfigurationProperty("dependencyType", IsRequired = true)]
        public string DependencyType => (string) this["dependencyType"];

        [ConfigurationProperty("lifestyle", IsRequired = false, DefaultValue = "Singleton")]
        public Lifestyle Lifestyle => Enum.TryParse(this["lifestyle"].ToString(), true, out Lifestyle result) ? result : Lifestyle.Singleton;

        protected override ConfigurationElement CreateNewElement()
        {
            return new ComponentRegistryCollectionImplementationTypeElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return Guid.NewGuid();
        }
    }
}