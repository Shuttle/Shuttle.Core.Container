using System.Collections.Generic;

namespace Shuttle.Core.Container
{
    public interface IComponentResolverConfiguration
    {
        IEnumerable<ComponentResolverConfiguration.Component> Components { get; }
    }
}