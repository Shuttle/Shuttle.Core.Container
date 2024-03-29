﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Shuttle.Core.Contract;
using Shuttle.Core.Logging;

namespace Shuttle.Core.Container
{
    public static class ComponentRegistryExtensions
    {
        public static readonly List<string> DefaultSuffixes = new List<string>
        {
            "Query",
            "Repository",
            "Provider",
            "Service",
            "Task",
            "Factory",
            "Mapper",
            "Cache"
        };

        /// <summary>
        ///     Determines whether the component registry has a dependency of the given type registered.
        /// </summary>
        /// <typeparam name="TDependency">The type of the dependency that is being checked.</typeparam>
        /// <param name="registry">The `IComponentRegistry` instance to register the mapping against.</param>
        /// <returns>Returns `true` if the dependency type is registered; else `false`.</returns>
        public static bool IsRegistered<TDependency>(this IComponentRegistry registry)
        {
            Guard.AgainstNull(registry, nameof(registry));

            return registry.IsRegistered(typeof(TDependency));
        }

        /// <summary>
        ///     Registers a new dependency/implementation type pair as a singleton.
        /// </summary>
        /// <typeparam name="TDependency">The type of the dependency being registered.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation that should be resolved.</typeparam>
        /// <param name="registry">The `IComponentRegistry` instance to register the mapping against.</param>
        public static IComponentRegistry Register<TDependency, TImplementation>(this IComponentRegistry registry)
            where TDependency : class
            where TImplementation : class, TDependency
        {
            Guard.AgainstNull(registry, nameof(registry));

            registry.Register<TDependency, TImplementation>(Lifestyle.Singleton);

            return registry;
        }

        /// <summary>
        ///     Registers a new dependency/implementation type pair.
        /// </summary>
        /// <typeparam name="TDependency">The type of the dependency being registered.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation that should be resolved.</typeparam>
        /// <param name="registry">The `IComponentRegistry` instance to register the mapping against.</param>
        /// <param name="lifestyle">The lifestyle of the component.</param>
        public static IComponentRegistry Register<TDependency, TImplementation>(this IComponentRegistry registry,
            Lifestyle lifestyle)
            where TDependency : class
            where TImplementation : TDependency
        {
            Guard.AgainstNull(registry, nameof(registry));

            registry.Register(typeof(TDependency), typeof(TImplementation), lifestyle);

            return registry;
        }

        /// <summary>
        ///     Registers a new dependency/implementation type pair as a singleton.
        /// </summary>
        /// <typeparam name="TDependencyImplementation">
        ///     The type of the dependency, that is also the implementation, being
        ///     registered.
        /// </typeparam>
        /// <param name="registry">The `IComponentRegistry` instance to register the mapping against.</param>
        public static IComponentRegistry Register<TDependencyImplementation>(this IComponentRegistry registry)
            where TDependencyImplementation : class
        {
            return registry.Register<TDependencyImplementation>(Lifestyle.Singleton);
        }

        /// <summary>
        ///     Registers a new dependency/implementation type pair.
        /// </summary>
        /// <typeparam name="TDependencyImplementation">
        ///     The type of the dependency, that is also the implementation, being
        ///     registered.
        /// </typeparam>
        /// <param name="registry">The `IComponentRegistry` instance to register the mapping against.</param>
        /// <param name="lifestyle">The lifestyle of the component.</param>
        public static IComponentRegistry Register<TDependencyImplementation>(this IComponentRegistry registry,
            Lifestyle lifestyle)
            where TDependencyImplementation : class
        {
            Guard.AgainstNull(registry, nameof(registry));

            registry.Register(typeof(TDependencyImplementation), typeof(TDependencyImplementation), lifestyle);

            return registry;
        }

        /// <summary>
        ///     Registers a singleton instance for the given dependency type.
        /// </summary>
        /// <typeparam name="TDependency">The type of the dependency being registered.</typeparam>
        /// <param name="registry">The `IComponentRegistry` instance to register the mapping against.</param>
        /// <param name="instance">The singleton instance to be registered.</param>
        public static IComponentRegistry RegisterInstance<TDependency>(this IComponentRegistry registry,
            TDependency instance)
        {
            Guard.AgainstNull(registry, nameof(registry));

            registry.RegisterInstance(typeof(TDependency), instance);

            return registry;
        }

        /// <summary>
        ///     Registers a new dependency/implementation type pair as a singleton.
        /// </summary>
        /// <param name="registry">The `IComponentRegistry` instance to register the mapping against.</param>
        /// <param name="dependencyType">The type of the dependency being registered.</param>
        /// <param name="implementationType">The type of the implementation that should be resolved.</param>
        /// <returns></returns>
        public static IComponentRegistry Register(this IComponentRegistry registry, Type dependencyType,
            Type implementationType)
        {
            Guard.AgainstNull(registry, nameof(registry));

            registry.Register(dependencyType, implementationType, Lifestyle.Singleton);

            return registry;
        }

        /// <summary>
        ///     Registers a new dependency/implementation type pair as a singleton if the dependency has not yet been registered;
        ///     else does nothing.
        /// </summary>
        /// <typeparam name="TDependency">The type of the dependency being registered.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation that should be resolved.</typeparam>
        /// <param name="registry">The `IComponentRegistry` instance to register the mapping against.</param>
        public static IComponentRegistry AttemptRegister<TDependency, TImplementation>(this IComponentRegistry registry)
            where TDependency : class
            where TImplementation : class, TDependency
        {
            Guard.AgainstNull(registry, nameof(registry));

            if (registry.IsRegistered<TDependency>())
            {
                return registry;
            }

            registry.Register<TDependency, TImplementation>(Lifestyle.Singleton);

            return registry;
        }

        /// <summary>
        ///     Registers a new dependency/implementation type pair if the dependency has not yet been registered; else does
        ///     nothing.
        /// </summary>
        /// <typeparam name="TDependency">The type of the dependency being registered.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation that should be resolved.</typeparam>
        /// <param name="registry">The `IComponentRegistry` instance to register the mapping against.</param>
        /// <param name="lifestyle">The lifestyle of the component.</param>
        public static IComponentRegistry AttemptRegister<TDependency, TImplementation>(this IComponentRegistry registry,
            Lifestyle lifestyle)
            where TDependency : class
            where TImplementation : TDependency
        {
            Guard.AgainstNull(registry, nameof(registry));

            if (registry.IsRegistered<TDependency>())
            {
                return registry;
            }

            registry.Register(typeof(TDependency), typeof(TImplementation), lifestyle);

            return registry;
        }

        /// <summary>
        ///     Registers a new dependency/implementation type pair as a singleton if the dependency has not yet been registered;
        ///     else does nothing.
        /// </summary>
        /// <typeparam name="TDependencyImplementation">
        ///     The type of the dependency, that is also the implementation, being
        ///     registered.
        /// </typeparam>
        /// <param name="registry">The `IComponentRegistry` instance to register the mapping against.</param>
        public static IComponentRegistry AttemptRegister<TDependencyImplementation>(this IComponentRegistry registry)
            where TDependencyImplementation : class
        {
            return registry.IsRegistered<TDependencyImplementation>()
                ? registry
                : registry.Register<TDependencyImplementation>(Lifestyle.Singleton);
        }

        /// <summary>
        ///     Registers a new dependency/implementation type pair if the dependency has not yet been registered; else does
        ///     nothing.
        /// </summary>
        /// <typeparam name="TDependencyImplementation">
        ///     The type of the dependency, that is also the implementation, being
        ///     registered.
        /// </typeparam>
        /// <param name="registry">The `IComponentRegistry` instance to register the mapping against.</param>
        /// <param name="lifestyle">The lifestyle of the component.</param>
        public static IComponentRegistry AttemptRegister<TDependencyImplementation>(this IComponentRegistry registry,
            Lifestyle lifestyle)
            where TDependencyImplementation : class
        {
            Guard.AgainstNull(registry, nameof(registry));

            if (registry.IsRegistered<TDependencyImplementation>())
            {
                return registry;
            }

            registry.Register(typeof(TDependencyImplementation), typeof(TDependencyImplementation), lifestyle);

            return registry;
        }

        /// <summary>
        ///     Registers a singleton instance for the given dependency type if the dependency has not yet been registered; else
        ///     does nothing.
        /// </summary>
        /// <typeparam name="TDependency">The type of the dependency being registered.</typeparam>
        /// <param name="registry">The `IComponentRegistry` instance to register the mapping against.</param>
        /// <param name="instance">The singleton instance to be registered.</param>
        public static IComponentRegistry AttemptRegisterInstance<TDependency>(this IComponentRegistry registry,
            TDependency instance)
        {
            Guard.AgainstNull(registry, nameof(registry));

            if (registry.IsRegistered<TDependency>())
            {
                return registry;
            }

            registry.RegisterInstance(typeof(TDependency), instance);

            return registry;
        }

        /// <summary>
        ///     Registers an open generic for the given dependency type as a singleton if the dependency has not yet been
        ///     registered; else
        ///     does nothing.
        /// </summary>
        /// <param name="registry">The `IComponentRegistry` instance to register the mapping against.</param>
        /// <param name="dependencyType">The open generic type of the dependency being registered.</param>
        /// <param name="implementationType">The open generic type of the implementation that should be resolved.</param>
        public static IComponentRegistry AttemptRegisterGeneric(this IComponentRegistry registry, Type dependencyType,
            Type implementationType)
        {
            return AttemptRegisterGeneric(registry, dependencyType, implementationType, Lifestyle.Singleton);
        }

        /// <summary>
        ///     Registers an open generic for the given dependency type if the dependency has not yet been registered; else
        ///     does nothing.
        /// </summary>
        /// <param name="registry">The `IComponentRegistry` instance to register the mapping against.</param>
        /// <param name="dependencyType">The open generic type of the dependency being registered.</param>
        /// <param name="implementationType">The open generic type of the implementation that should be resolved.</param>
        /// <param name="lifestyle">The lifestyle of the component.</param>
        public static IComponentRegistry AttemptRegisterGeneric(this IComponentRegistry registry, Type dependencyType,
            Type implementationType, Lifestyle lifestyle)
        {
            Guard.AgainstNull(registry, nameof(registry));

            if (registry.IsRegistered(dependencyType))
            {
                return registry;
            }

            registry.RegisterGeneric(dependencyType, implementationType, lifestyle);

            return registry;
        }

        public static IComponentRegistry AttemptRegisterComponentResolverDelegate(this IComponentRegistry registry)
        {
            if (!registry.IsRegistered<IComponentResolver>())
            {
                Log.Information(Resources.ComponentResolverDelegateRegistered);

                registry.AttemptRegisterInstance<IComponentResolver>(new ComponentResolverDelegate());
            }

            return registry;
        }

        /// <summary>
        ///     Register all types in the given assembly that end in the `DefaultSuffixes` against a dependency type matching the
        ///     type name with an `I` prefix, e.g. `CustomerRepository` will be registered against `ICustomerRepository`.
        /// </summary>
        /// <param name="registry">The `IComponentRegistry` instance to register the mapping against.</param>
        /// <param name="assemblyName">The assembly name that contains the types to evaluate.</param>
        /// <param name="lifestyle">The `Lifestyle` to register the dependency as.  The default is `Lifestyle.Singleton`.</param>
        public static void RegisterSuffixed(this IComponentRegistry registry, string assemblyName,
            Lifestyle lifestyle = Lifestyle.Singleton)
        {
            RegisterSuffixed(registry, assemblyName, DefaultSuffixes, lifestyle);
        }

        /// <summary>
        ///     Register all types in the given assembly that end in the `DefaultSuffixes` against a dependency type matching the
        ///     type name with an `I` prefix, e.g. `CustomerRepository` will be registered against `ICustomerRepository`.
        /// </summary>
        /// <param name="registry">The `IComponentRegistry` instance to register the mapping against.</param>
        /// <param name="assembly">The assembly that contains the types to evaluate.</param>
        /// <param name="lifestyle">The `Lifestyle` to register the dependency as.  The default is `Lifestyle.Singleton`.</param>
        public static void RegisterSuffixed(this IComponentRegistry registry, Assembly assembly,
            Lifestyle lifestyle = Lifestyle.Singleton)
        {
            RegisterSuffixed(registry, assembly, DefaultSuffixes, lifestyle);
        }

        /// <summary>
        ///     Register all types in the given assembly that end in the given `suffixes` against a dependency type matching the
        ///     type name with an `I` prefix, e.g. `CustomerRepository` will be registered against `ICustomerRepository`.
        /// </summary>
        /// <param name="registry">The `IComponentRegistry` instance to register the mapping against.</param>
        /// <param name="assemblyName">The assembly name that contains the types to evaluate.</param>
        /// <param name="suffixes">A list of suffixes that a type should end in to be registered.</param>
        /// <param name="lifestyle">The `Lifestyle` to register the dependency as.  The default is `Lifestyle.Singleton`.</param>
        public static void RegisterSuffixed(this IComponentRegistry registry, string assemblyName,
            IEnumerable<string> suffixes, Lifestyle lifestyle = Lifestyle.Singleton)
        {
            Guard.AgainstNullOrEmptyString(assemblyName, nameof(assemblyName));

            RegisterSuffixed(registry, Assembly.Load(assemblyName), suffixes, lifestyle);
        }

        /// <summary>
        ///     Register all types in the given assembly that end in the given `suffixes` against a dependency type matching the
        ///     type name with an `I` prefix, e.g. `CustomerRepository` will be registered against `ICustomerRepository`.
        /// </summary>
        /// <param name="registry">The `IComponentRegistry` instance to register the mapping against.</param>
        /// <param name="assembly">The assembly that contains the types to evaluate.</param>
        /// <param name="suffixes">A list of suffixes that a type should end in to be registered.</param>
        /// <param name="lifestyle">The `Lifestyle` to register the dependency as.  The default is `Lifestyle.Singleton`.</param>
        public static void RegisterSuffixed(this IComponentRegistry registry, Assembly assembly,
            IEnumerable<string> suffixes, Lifestyle lifestyle = Lifestyle.Singleton)
        {
            Guard.AgainstNull(registry, nameof(registry));
            Guard.AgainstNull(assembly, nameof(assembly));
            Guard.AgainstNull(suffixes, nameof(suffixes));

            var enumerable = suffixes.ToList();

            Register(
                registry,
                assembly,
                type => enumerable.Any(suffix => type.Name.EndsWith(suffix)),
                type =>
                {
                    var interfaces = type.GetInterfaces();

                    if (interfaces.Length == 0)
                    {
                        return null;
                    }

                    return interfaces.FirstOrDefault(item => item.Name.Equals($"I{type.Name}")) ?? interfaces.First();
                },
                type => lifestyle);
        }

        /// <summary>
        ///     Registers all the types in the given assembly that satisfies the `shouldRegister` function against the type
        ///     returned
        ///     from the `getDependencyType` function.
        /// </summary>
        /// <param name="registry">The `IComponentRegistry` instance to register the mapping against.</param>
        /// <param name="assembly">The assembly that contains the types to evaluate.</param>
        /// <param name="shouldRegister">A function that returns `true` to register the type; else `false` to ignore the type.</param>
        /// <param name="getDependencyType">
        ///     A function that returns the dependency `Type` that the implementation type should be
        ///     registered against; else `null` to ignore the register for no qualifying dependency type.
        /// </param>
        /// <param name="getLifestyle">A function that returns the `Lifestyle` with which to register the component.</param>
        public static void Register(this IComponentRegistry registry, Assembly assembly,
            Func<Type, bool> shouldRegister, Func<Type, Type> getDependencyType, Func<Type, Lifestyle> getLifestyle)
        {
            Guard.AgainstNull(registry, nameof(registry));
            Guard.AgainstNull(assembly, nameof(assembly));
            Guard.AgainstNull(shouldRegister, nameof(shouldRegister));
            Guard.AgainstNull(getDependencyType, nameof(getDependencyType));
            Guard.AgainstNull(getLifestyle, nameof(getLifestyle));

            var registrations = new Dictionary<Type, List<Type>>();

            foreach (var type in assembly.GetTypes())
            {
                if (type.IsInterface || type.IsAbstract || !shouldRegister.Invoke(type))
                {
                    continue;
                }

                var interfaceType = getDependencyType.Invoke(type);

                if (interfaceType == null)
                {
                    continue;
                }

                if (!registrations.ContainsKey(interfaceType))
                {
                    registrations.Add(interfaceType, new List<Type>());
                }

                registrations[interfaceType].Add(type);
            }

            foreach (var registration in registrations)
            {
                if (registration.Value.Count == 1)
                {
                    registry.Register(registration.Key, registration.Value.First(),
                        getLifestyle.Invoke(registration.Key));
                }
                else
                {
                    registry.RegisterCollection(registration.Key, registration.Value,
                        getLifestyle.Invoke(registration.Key));
                }
            }
        }

        /// <summary>
        ///     Register all interfaces that are not yet registered in the given assembly that have a single implementation as a
        ///     regular dependency.
        ///     All interfaces that have more than 1 implementation are registered as collections.
        /// </summary>
        /// <param name="registry">The `IComponentRegistry` instance to register the mapping against.</param>
        /// <param name="assemblyName">The assembly name that contains the types to evaluate.</param>
        /// <param name="lifestyle">The `Lifestyle` to register the dependency as.  The default is `Lifestyle.Singleton`.</param>
        public static void RegisterAll(this IComponentRegistry registry, string assemblyName,
            Lifestyle lifestyle = Lifestyle.Singleton)
        {
            RegisterAll(registry, Assembly.Load(assemblyName), lifestyle);
        }

        /// <summary>
        ///     Register all interfaces that are not yet registered in the given assembly that have a single implementation as a
        ///     regular dependency.
        ///     All interfaces that have more than 1 implementation are registered as collections.
        /// </summary>
        /// <param name="registry">The `IComponentRegistry` instance to register the mapping against.</param>
        /// <param name="assembly">The assembly that contains the types to evaluate.</param>
        /// <param name="lifestyle">The `Lifestyle` to register the dependency as.</param>
        public static void RegisterAll(this IComponentRegistry registry, Assembly assembly,
            Lifestyle lifestyle = Lifestyle.Singleton)
        {
            Guard.AgainstNull(registry, nameof(registry));
            Guard.AgainstNull(assembly, nameof(assembly));

            Type FindDependencyType(Type type)
            {
                var interfaces = type.GetInterfaces();

                if (interfaces.Length == 0)
                {
                    return null;
                }

                return interfaces.FirstOrDefault(item => item.Name.Equals($"I{type.Name}")) ?? interfaces.First();
            }

            Register(
                registry,
                assembly,
                type =>
                {
                    var dependencyType = FindDependencyType(type);

                    return dependencyType != null && !registry.IsRegistered(dependencyType);
                },
                FindDependencyType,
                type => lifestyle);
        }

        /// <summary>
        ///     Attempts to register all components specified in the application configuration file `componentRegistry` section.
        /// </summary>
        /// <param name="registry">The `IComponentRegistry` instance to register the mapping against.</param>
        public static void AttemptRegisterConfiguration(this IComponentRegistry registry)
        {
            AttemptRegisterConfiguration(registry, ComponentRegistrySection.GetConfiguration());
        }

        /// <summary>
        ///     Attempts to register all components specified in the given `IComponentRegistryConfiguration` instance.
        /// </summary>
        /// <param name="registry">The `IComponentRegistry` instance to register the mapping against.</param>
        /// <param name="registryConfiguration">
        ///     The `IComponentRegistryConfiguration` instance that contains the registry
        ///     configuration.
        /// </param>
        public static void AttemptRegisterConfiguration(this IComponentRegistry registry, IComponentRegistryConfiguration registryConfiguration)
        {
            Guard.AgainstNull(registry, nameof(registry));

            foreach (var component in registryConfiguration.Components)
            {
                registry.Register(component.DependencyType, component.ImplementationType, component.Lifestyle);
            }

            foreach (var collection in registryConfiguration.Collections)
            {
                registry.RegisterCollection(collection.DependencyType, collection.ImplementationTypes,
                    collection.Lifestyle);
            }
        }
    }
}