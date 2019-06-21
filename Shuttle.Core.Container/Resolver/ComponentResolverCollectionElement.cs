using System;
using System.Configuration;

namespace Shuttle.Core.Container
{
    public class ComponentResolverCollectionElement : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ComponentResolverElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return Guid.NewGuid();
        }
    }
}