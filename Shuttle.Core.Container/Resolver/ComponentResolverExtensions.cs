using System;
using System.Collections.Generic;
using System.Linq;
using Shuttle.Core.Contract;

namespace Shuttle.Core.Container
{
    public static class ComponentResolverExtensions
    {
        /// <summary>
        ///     Resolves the requested service type.  If the service type cannot be resolved an exception is thrown.
        /// </summary>
        /// <typeparam name="T">The type of the service that should be resolved.</typeparam>
        /// <param name="resolver">The resolver instance that contains the registered service.</param>
        /// <returns>An instance of the type implementing the requested service type.</returns>
        public static T Resolve<T>(this IComponentResolver resolver) where T : class
        {
            Guard.AgainstNull(resolver, nameof(resolver));

            return (T)resolver.Resolve(typeof(T));
        }

        /// <summary>
        ///     Attempts to resolve the requested service type.  If the service type cannot be resolved null is returned.
        /// </summary>
        /// <typeparam name="T">The type of the service that should be resolved.</typeparam>
        /// <param name="resolver">The resolver instance that contains the registered service.</param>
        /// <returns>An instance of the type implementing the requested service type if it can be resolved; else null.</returns>
        public static T AttemptResolve<T>(this IComponentResolver resolver) where T : class
        {
            return (T)AttemptResolve(resolver, typeof(T));
        }

        /// <summary>
        ///     Attempts to resolve the requested service type.  If the service type cannot be resolved null is returned.
        /// </summary>
        /// <param name="resolver">The resolver instance that contains the registered service.</param>
        /// <param name="dependencyType">>The type of the service that should be resolved.</param>
        /// <returns>An instance of the type implementing the requested service type if it can be resolved; else null.</returns>
        public static object AttemptResolve(this IComponentResolver resolver, Type dependencyType)
        {
            Guard.AgainstNull(resolver, nameof(resolver));
            Guard.AgainstNull(dependencyType, nameof(dependencyType));

            try
            {
                return resolver.Resolve(dependencyType);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        ///     Resolves all the given types.  These may be types that will not necessarily be injected into another class but that
        ///     may require other instances from the resolver.
        /// </summary>
        /// <param name="resolver">The resolver instance that contains the registered services.</param>
        /// <param name="dependencyTypes">The list of service types that need to be resolved.</param>
        public static IEnumerable<object> Resolve(this IComponentResolver resolver, IEnumerable<Type> dependencyTypes)
        {
            Guard.AgainstNull(resolver, nameof(resolver));

            var result = new List<object>();
            var types = dependencyTypes as IList<Type> ?? dependencyTypes.ToList();

            if (!types.Any())
            {
                return result;
            }

            result.AddRange(types.Select(resolver.Resolve));

            return result;
        }

        /// <summary>
        ///     Resolves all registered instances of the requested service type.
        /// </summary>
        /// <typeparam name="T">The type of the services that should be resolved.</typeparam>
        /// <param name="resolver">The resolver instance that contains the registered service.</param>
        /// <returns>All instances of the types implementing the requested service type.</returns>
        public static IEnumerable<T> ResolveAll<T>(this IComponentResolver resolver) where T : class
        {
            Guard.AgainstNull(resolver, nameof(resolver));

            return resolver.ResolveAll(typeof(T)).Cast<T>().ToList();
        }

        public static IComponentResolver WireComponentResolverDelegate(this IComponentResolver resolver)
        {
            try
            {
                var registered = resolver.Resolve<IComponentResolver>();

                if (registered is ComponentResolverDelegate componentResolver)
                {
                    componentResolver.Assign(resolver);
                }
            }
            catch
            {
                // ignore - assume container implementation registered itself
            }

            return resolver;
        }

        /// <summary>
        ///     Resolves all dependencies specified in the application configuration file `componentResolver` section.
        /// </summary>
        /// <param name="resolver">The `IComponentResolver` instance to resolve dependencies from.</param>
        public static void ResolveConfiguration(this IComponentResolver resolver)
        {
            ResolveConfiguration(resolver, ComponentResolverSection.GetConfiguration());
        }

        /// <summary>
        ///     Resolves all dependencies specified in the given `IComponentResolverConfiguration` instance.
        /// </summary>
        /// <param name="resolver">The `IComponentResolver` instance to resolve dependencies from.</param>
        /// <param name="resolverConfiguration">The `IComponentResolverConfiguration` instance that contains the registry configuration.</param>
        public static void ResolveConfiguration(IComponentResolver resolver, IComponentResolverConfiguration resolverConfiguration)
        {
            Guard.AgainstNull(resolver, nameof(resolver));
            Guard.AgainstNull(resolverConfiguration, nameof(resolverConfiguration));

            foreach (var component in resolverConfiguration.Components)
            {
                resolver.Resolve(component.DependencyType);
            }

            resolver.WireComponentResolverDelegate();
        }
    }
}