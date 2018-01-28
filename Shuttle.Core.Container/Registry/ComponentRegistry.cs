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

        private IComponentRegistry Register(Type dependencyType)
        {
            Guard.AgainstNull(dependencyType, nameof(dependencyType));

            if (IsRegistered(dependencyType))
            {
                throw new TypeRegistrationException(
                    string.Format(Resources.DuplicateTypeRegistrationException, dependencyType.FullName));
            }

            _registeredTypes.Add(dependencyType);

            return this;
        }

        public virtual IComponentRegistry Register(Type dependencyType, Type implementationType, Lifestyle lifestyle)
        {
            return Register(dependencyType);
        }

        public virtual IComponentRegistry RegisterGeneric(Type dependencyType, Type implementationType, Lifestyle lifestyle)
        {
            return Register(dependencyType);
        }

        public virtual IComponentRegistry RegisterCollection(Type dependencyType, IEnumerable<Type> implementationTypes,
            Lifestyle lifestyle)
        {
            Guard.AgainstNull(implementationTypes, nameof(implementationTypes));

            return Register(dependencyType);
        }

        public virtual IComponentRegistry RegisterInstance(Type dependencyType, object instance)
        {
            Guard.AgainstNull(instance, nameof(instance));

            return Register(dependencyType);
        }
    }
}