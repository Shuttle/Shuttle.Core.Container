
# Shuttle.Core.Container

```
PM> Install-Package Shuttle.Core.Container
```

In the dependency injection (DI) world there appears to be somewhat of a trend to separate registration and resolution of components.  Some containers have an explicit split while others do not allow any registrations after the first instance resolution.

To this end the `Shuttle.Core.Container` package provides two interfaces that relate to dependency injection containers.  The `IComponentRegistry` defines the registration of dependencies while the `IComponentResolver` defines the resolution of dependencies.

Typically there would be no need to directly reference this package unless you are developing an adapter to a dependency injection container.  Instead you would reference one of the implementations directly.

## IComponentRegistry

### Lifestyle

```
public enum Lifestyle
{
    Singleton = 0,Transient = 1
}
```

When registering a dependency type with an implementation type you can specify one of the above lifestyles for your component:

- Singleton: only a single instance is created and that instance is returned for any call to `Resolve` the service type.
- Transient: a new instance of the implementation type is returned for each call to `Resolve` the service type.

### Registering all dependency implementations

``` c#
public static void RegisterAll(this IComponentRegistry registry, string assemblyName, Lifestyle lifestyle = Lifestyle.Singleton)
public static void RegisterAll(this IComponentRegistry registry, Assembly assembly, Lifestyle lifestyle = Lifestyle.Singleton)
```

Register all interfaces in the given assembly, that have not yet been registered, that have a single implementation as a regular dependency.  All interfaces that have more than 1 implementation are registered as collections.

This method differes from the usual convention in that there is no `AttemptRegisterAll` method.

### Register

``` c#
IComponentRegistry Register(Type dependencyType, Type implementationType, Lifestyle lifestyle);
```

Registers a dependency by type with the relevant lifestyle.

``` c#
IComponentRegistry Register(Type dependencyType, object instance);
```

Registers the given singleton instance against the dependency type.

``` c#
IComponentRegistry RegisterCollection(Type dependencyType, IEnumerable<Type> implementationTypes, Lifestyle lifestyle);
```

Registers a collection of implementation types against the relevant dependency type using the given lifestyle.  Collections are a somewhat unique case and need to be registered as such.

#### Extensions

There are a number of extension methods that facilitate the registration of components:

```c#
public static bool IsRegistered<TDependency>(this IComponentRegistry registry)
```

Will return `true` if the dependency is registered; else `false`.

```c#
public static IComponentRegistry Register<TDependency, TImplementation>(this IComponentRegistry registry)
```

Registers a new dependency/implementation type pair as a singleton.

```c#
public static IComponentRegistry Register<TDependency, TImplementation>(this IComponentRegistry registry, Lifestyle lifestyle)
```

Registers a new dependency/implementation type pair with the given `Lifestyle`.

```c#
public static IComponentRegistry Register<TDependencyImplementation>(this IComponentRegistry registry)
```

Registers a new dependency/implementation type pair by binding the implementation type to itself.

```c#
public static IComponentRegistry RegisterInstance<TDependency>(this IComponentRegistry registry, TDependency instance)
```

Registers a singleton instance for the given dependency type.

```c#
public static IComponentRegistry Register(this IComponentRegistry registry, Type dependencyType, Type implementationType)
```

Registers a new dependency/implementation type pair as a singleton.

```c#
public static IComponentRegistry AttemptRegister<TDependency, TImplementation>(this IComponentRegistry registry)
```

Registers a new dependency/implementation type pair as a singleton if the dependency has not yet been registered; else does nothing.

```c#
public static IComponentRegistry AttemptRegister<TDependency, TImplementation>(this IComponentRegistry registry)
```

Registers a new dependency/implementation type pair as a singleton if the dependency has not yet been registered; else does nothing.

```c#
public static IComponentRegistry AttemptRegister<TDependency, TImplementation>(this IComponentRegistry registry, Lifestyle lifestyle)
```

Registers a new dependency/implementation type pair if the dependency has not yet been registered; else does nothing.

```c#
public static IComponentRegistry AttemptRegister<TDependencyImplementation>(this IComponentRegistry registry)
```

Registers a new dependency/implementation type pair as a singleton if the dependency has not yet been registered; else does nothing.

```c#
public static IComponentRegistry AttemptRegister<TDependencyImplementation>(this IComponentRegistry registry, Lifestyle lifestyle)
```

Registers a new dependency/implementation type pair if the dependency has not yet been registered; else does nothing.

```c#
public static IComponentRegistry AttemptRegister<TDependencyImplementation>(this IComponentRegistry registry, Lifestyle lifestyle)
```

Registers a new dependency/implementation type pair if the dependency has not yet been registered; else does nothing.

```c#
public static IComponentRegistry AttemptRegister<TDependencyImplementation>(this IComponentRegistry registry, Lifestyle lifestyle)
```

Registers a new dependency/implementation type pair if the dependency has not yet been registered; else does nothing.

```c#
public static IComponentRegistry AttemptRegister<TDependencyImplementation>(this IComponentRegistry registry, Lifestyle lifestyle)
```

Registers a new dependency/implementation type pair with then given `Lifestyle` if the dependency has not yet been registered; else does nothing.

```c#
public static IComponentRegistry AttemptRegisterInstance<TDependency>(this IComponentRegistry registry, TDependency instance)
```

Registers a singleton instance for the given dependency type if the dependency has not yet been registered; else does nothing.

```c#
public static IComponentRegistry AttemptRegisterGeneric(this IComponentRegistry registry, Type dependencyType, Type implementationType)
```

Registers an open generic for the given dependency type as a singleton if the dependency has not yet been registered; else does nothing.

```c#
public static IComponentRegistry AttemptRegisterGeneric(this IComponentRegistry registry, Type dependencyType, Type implementationType, Lifestyle lifestyle)
```

Registers an open generic for the given dependency type if the dependency has not yet been registered; else does nothing.

```c#
public static void RegisterSuffixed(this IComponentRegistry registry, string assemblyName, Lifestyle lifestyle = Lifestyle.Singleton)
public static void RegisterSuffixed(this IComponentRegistry registry, Assembly assembly, Lifestyle lifestyle = Lifestyle.Singleton)
```

Register all types in the given assembly that end in the `DefaultSuffixes` against a dependency type matching the type name with an `I` prefix, e.g. `CustomerRepository` will be registered against `ICustomerRepository`.

```c#
public static void RegisterSuffixed(this IComponentRegistry registry, string assemblyName, IEnumerable<string> suffixes, Lifestyle lifestyle = Lifestyle.Singleton)
public static void RegisterSuffixed(this IComponentRegistry registry, Assembly assembly, IEnumerable<string> suffixes, Lifestyle lifestyle = Lifestyle.Singleton)
```

Register all types in the given assembly that end in the given `suffixes` against a dependency type matching the type name with an `I` prefix, e.g. `CustomerRepository` will be registered against `ICustomerRepository`.

```c#
public static void Register(this IComponentRegistry registry, Assembly assembly, Func<Type, bool> shouldRegister, Func<Type, Type> getDependencyType, Func<Type, Lifestyle> getLifestyle)
```

Registers all the types in the given assembly that satisfies the `shouldRegister` function against the type returned from the `getDependencyType` function.

## IComponentResolver

### Resolve

``` c#
object Resolve(Type dependencyType);
```

The requested dependency type will be resolved by returning the relevant instance of the implementation type.  

``` c#
IEnumerable<object> ResolveAll(Type dependencyType);
```

All instances of the requested dependency type will be resolved.  

#### Extensions

There are a couple of extension methods available to facilitate the resolving of dependencies:

```c#
public static T Resolve<T>(this IComponentResolver resolver)
```

Resolves the requested service type.  If the service type cannot be resolved an exception is thrown.

```c#
public static T AttemptResolve<T>(this IComponentResolver resolver)
public static object AttemptResolve(this IComponentResolver resolver, Type dependencyType)
```

Attempts to resolve the requested service type.  If the service type cannot be resolved null is returned.

```c#
public static IEnumerable<object> Resolve(this IComponentResolver resolver, IEnumerable<Type> dependencyTypes)
```

Resolves all the given types.  These may be types that will not necessarily be injected into another class but that may require other instances from the resolver.

```c#
public static IEnumerable<T> ResolveAll<T>(this IComponentResolver resolver)
```

Resolves all registered instances of the requested service type.

<a name="Supported"></a>

## Implementations

The following implementations can be used *out-of-the-box*:

- [WindsorContainer](https://github.com/Shuttle/Shuttle.Core.Castle)
- [Ninject](https://github.com/Shuttle/Shuttle.Core.Ninject)
- [AutoFac](https://github.com/Shuttle/Shuttle.Core.Autofac)
- [StructureMap](https://github.com/Shuttle/Shuttle.Core.StructureMap)
- [SimpleInjector](https://github.com/Shuttle/Shuttle.Core.SimpleInjector)
- [Unity](https://github.com/Shuttle/Shuttle.Core.Unity)

If you don't see your container of choice here please [log an issue](https://github.com/Shuttle/Shuttle.Core.Container/issues/new) or share your own implementation.

<a name="Bootstrapping"></a>

## Bootstrapping (removed/moved)

The bootstrapping has been removed from `Shuttle.Core.Container` and moved to its own package.  If you would like to continue using this functionality you should make use of the new `Shuttle.Core.Container.Bootstrapping` package.
