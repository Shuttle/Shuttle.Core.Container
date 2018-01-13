using System;
using System.Collections.Generic;
using Shuttle.Core.Contract;

namespace Shuttle.Core.Container
{
    public abstract class ComponentRegistry : IComponentRegistry
    {
        private readonly List<Type> _registeredTypes = new List<Type>();

        public bool IsRegistered(Type type)
        {
            Guard.AgainstNull(type, nameof(type));

            return _registeredTypes.Contains(type);
        }

        public virtual IComponentRegistry Register(Type dependencyType, Type implementationType, Lifestyle lifestyle)
        {
            Guard.AgainstNull(dependencyType, nameof(dependencyType));
            Guard.AgainstNull(implementationType, nameof(implementationType));

            DependencyInvariant(dependencyType);

            _registeredTypes.Add(dependencyType);

            return this;
        }

        public virtual IComponentRegistry RegisterCollection(Type dependencyType, IEnumerable<Type> implementationTypes,
            Lifestyle lifestyle)
        {
            Guard.AgainstNull(dependencyType, nameof(dependencyType));
            Guard.AgainstNull(implementationTypes, nameof(implementationTypes));

            DependencyInvariant(dependencyType);

            _registeredTypes.Add(dependencyType);

            return this;
        }

        public virtual IComponentRegistry RegisterInstance(Type dependencyType, object instance)
        {
            Guard.AgainstNull(dependencyType, nameof(dependencyType));
            Guard.AgainstNull(instance, nameof(instance));

            DependencyInvariant(dependencyType);

            _registeredTypes.Add(dependencyType);

            return this;
        }

        private void DependencyInvariant(Type type)
        {
            if (IsRegistered(type))
            {
                throw new TypeRegistrationException(
                    string.Format(Resources.DuplicateTypeRegistrationException, type.FullName));
            }
        }
    }
}