﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Shuttle.Core.Container {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Shuttle.Core.Container.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Could not find an assembly with name &apos;{0}&apos;..
        /// </summary>
        public static string AssemblyNameNotFound {
            get {
                return ResourceManager.GetString("AssemblyNameNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No implementation of &apos;IComponentResolver&apos; has been registered.  The default &apos;ComponentResolver&apos; has been registered.  If you experience issues perhaps check the imlementation of your component container or log an issue..
        /// </summary>
        public static string ComponentResolverRegistered {
            get {
                return ResourceManager.GetString("ComponentResolverRegistered", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} &apos;{1}&apos; has no default constructor..
        /// </summary>
        public static string DefaultConstructorRequired {
            get {
                return ResourceManager.GetString("DefaultConstructorRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An implementation type has already been registered for dependency type &apos;{0}&apos;. .
        /// </summary>
        public static string DuplicateTypeRegistrationException {
            get {
                return ResourceManager.GetString("DuplicateTypeRegistrationException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No implementation types has been specified for the collection dependency of type &apos;{0}&apos;...
        /// </summary>
        public static string EmptyCollectionImplementationTypes {
            get {
                return ResourceManager.GetString("EmptyCollectionImplementationTypes", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No `IComponentResolver` instance has been registered.  Any component that is dependent on `IComponentResolver` will fail..
        /// </summary>
        public static string IComponentResolverNotRegistered {
            get {
                return ResourceManager.GetString("IComponentResolverNotRegistered", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Could not get type &apos;{0}&apos;.  Ensure that you have a fully qualified type: &apos;FullName, Assembly&apos;..
        /// </summary>
        public static string MissingTypeException {
            get {
                return ResourceManager.GetString("MissingTypeException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No implementation type has been registered for dependency type &apos;{0}&apos;..
        /// </summary>
        public static string TypeNotRegisteredException {
            get {
                return ResourceManager.GetString("TypeNotRegisteredException", resourceCulture);
            }
        }
    }
}
