using System;
using System.Configuration;

namespace Shuttle.Core.Container
{
    public class ComponentRegistryComponentsElement : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ComponentRegistryComponentElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return Guid.NewGuid();
        }
    }
}