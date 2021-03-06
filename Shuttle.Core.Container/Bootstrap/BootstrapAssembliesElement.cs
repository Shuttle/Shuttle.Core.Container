using System;
using System.Configuration;

namespace Shuttle.Core.Container
{
    public class BootstrapAssembliesElement : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new BootstrapAssemblyElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return Guid.NewGuid();
        }
    }
}