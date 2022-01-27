using System;
using System.Configuration;
using Shuttle.Core.Configuration;

namespace Shuttle.Core.Container
{
    public class ComponentResolverSection : ConfigurationSection
    {
        [ConfigurationProperty("components", IsRequired = false, DefaultValue = null)]
        public ComponentResolverCollectionElement Components => (ComponentResolverCollectionElement) this["components"];

        public static IComponentResolverConfiguration GetConfiguration()
        {
            var result = new ComponentResolverConfiguration();
            var section = ConfigurationSectionProvider.Open<ComponentResolverSection>("shuttle", "componentResolver");

            if (section == null)
            {
                return result;
            }

            foreach (ComponentResolverElement component in section.Components)
            {
                var dependencyType = Type.GetType(component.DependencyType);

                if (dependencyType == null)
                {
                    throw new ConfigurationErrorsException(
                        string.Format(Resources.MissingTypeException, component.DependencyType));
                }

                result.AddComponent(new ComponentResolverConfiguration.Component(dependencyType));
            }

            return result;
        }
    }
}