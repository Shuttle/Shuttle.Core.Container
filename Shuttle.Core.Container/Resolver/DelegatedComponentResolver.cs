using System;
using System.Collections.Generic;
using Shuttle.Core.Contract;

namespace Shuttle.Core.Container
{
    public class DelegatedComponentResolver : IComponentResolver
    {
        private readonly Func<Type, object> _resolve;
        private readonly Func<Type, IEnumerable<object>> _resolveAll;

        public DelegatedComponentResolver(Func<Type, object> resolve, Func<Type, IEnumerable<object>> resolveAll)
        {
            Guard.AgainstNull(resolve, nameof(resolve));
            Guard.AgainstNull(resolveAll, nameof(resolveAll));

            _resolve = resolve;
            _resolveAll = resolveAll;
        }

        public object Resolve(Type dependencyType)
        {
            Guard.AgainstNull(dependencyType, nameof(dependencyType));

            return _resolve.Invoke(dependencyType);
        }

        public IEnumerable<object> ResolveAll(Type dependencyType)
        {
            Guard.AgainstNull(dependencyType, nameof(dependencyType));

            return _resolveAll.Invoke(dependencyType);
        }
    }
}