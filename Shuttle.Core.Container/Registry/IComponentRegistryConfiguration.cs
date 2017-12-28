using System.Collections.Generic;

namespace Shuttle.Core.Container
{
    public interface IComponentRegistryConfiguration
    {
        IEnumerable<ComponentRegistryConfiguration.Component> Components { get; }
        IEnumerable<ComponentRegistryConfiguration.Collection> Collections { get; }
    }
}