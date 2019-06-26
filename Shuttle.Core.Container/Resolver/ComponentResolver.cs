using System;
using System.Collections.Generic;
using Shuttle.Core.Contract;

namespace Shuttle.Core.Container
{
    public class ComponentResolver : IComponentResolver
    {
        private IComponentResolver _resolver;

        public void Assign(IComponentResolver resolver)
        {
            Guard.AgainstNull(resolver, nameof(resolver));

            _resolver = resolver;
        }

        public object Resolve(Type dependencyType)
        {
            Guard.AgainstNull(dependencyType, nameof(dependencyType));

            return _resolver.Resolve(dependencyType);
        }

        public IEnumerable<object> ResolveAll(Type dependencyType)
        {
            Guard.AgainstNull(dependencyType, nameof(dependencyType));

            return _resolver.ResolveAll(dependencyType);
        }
    }
}