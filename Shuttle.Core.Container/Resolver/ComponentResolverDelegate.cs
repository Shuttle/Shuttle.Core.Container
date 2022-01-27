using System;
using System.Collections.Generic;
using Shuttle.Core.Contract;

namespace Shuttle.Core.Container
{
    public class ComponentResolverDelegate : IComponentResolver
    {
        private IComponentResolver _resolver;

        public object Resolve(Type dependencyType)
        {
            Guard.AgainstNull(dependencyType, nameof(dependencyType));

            return GuardedResolver().Resolve(dependencyType);
        }

        public IEnumerable<object> ResolveAll(Type dependencyType)
        {
            Guard.AgainstNull(dependencyType, nameof(dependencyType));

            return GuardedResolver().ResolveAll(dependencyType);
        }

        public void Assign(IComponentResolver resolver)
        {
            Guard.AgainstNull(resolver, nameof(resolver));

            _resolver = resolver;
        }

        private IComponentResolver GuardedResolver()
        {
            if (_resolver == null)
            {
                throw new TypeResolutionException(Resources.ComponentResolverProxyException);
            }

            return _resolver;
        }
    }
}