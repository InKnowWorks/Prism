using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using IKW.Contropolus.Prism.CastleWindsor.WPF.Legacy;
using Prism.Ioc;

namespace IKW.Contropolus.Prism.CastleWindsor.WPF.Ioc
{
    using System.ComponentModel;
    using System.Globalization;
    using global::Prism.Logging;
    using global::Prism.Modularity;
    using global::Prism.Regions;
    using global::Prism.Regions.Behaviors;
    using Wpf.Local.Messages;
    using Component = Castle.MicroKernel.Registration.Component;

    /// <summary>
    /// 
    /// </summary>
    public class CastleWindsorContainerExtension : IContainerExtension<IWindsorContainer>
    {
        /// <summary>
        /// 
        /// </summary>
        public IWindsorContainer Instance { get; }

        /// <summary>
        /// 
        /// </summary>
        public CastleWindsorContainerExtension() : this(new WindsorContainer()) { }

        /// <summary>
        /// Must register the Windsor Castle container as a singleton 
        /// </summary>
        /// <param name="container"></param>
        public CastleWindsorContainerExtension(IWindsorContainer container)
        {
            Instance = container;

            Instance.Register(Component.For<IWindsorContainer>()
                .Instance(container)
                .Named(container.Name)
                .LifeStyle.Singleton);

            // register region adapters
            Instance.Register(Classes.FromAssemblyContaining<IRegionAdapter>().BasedOn<IRegionAdapter>().LifestyleTransient());

            // register region behaviors
            Instance.Register(Classes.FromAssemblyContaining<IRegionBehavior>().BasedOn<IRegionBehavior>().LifestyleTransient());

            //Instance.RegisterType<DelayedRegionCreationBehavior,DelayedRegionCreationBehavior>(false);
        }

        /// <summary>
        /// 
        /// </summary>
        public void FinalizeExtension() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        public IContainerRegistry RegisterInstance(Type type, object instance)
        {
            if (!Instance.Kernel.HasComponent(type))
            {
                Instance.Register(Component.For(type)
                    .Instance(instance)
                    .LifeStyle.Transient);
            }

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="instance"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public IContainerRegistry RegisterInstance(Type type, object instance, string name)
        {
            if (!Instance.Kernel.HasComponent(type) &&
                !Instance.Kernel.HasComponent(name))
            {
                Instance.Register(Component.For(type)
                    .Instance(instance)
                    .Named(name)
                    .LifeStyle.Transient);
            }

            return this;
        }

        /// <summary>
        /// Register a TServiceImplementation object type instance as a singleton
        /// </summary>
        /// <typeparam name="TServiceImplementation"></typeparam>
        /// <param name="instance"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public IContainerRegistry RegisterSingletonType<TServiceImplementation>(object instance, string name) where TServiceImplementation: class
        {
            if (!Instance.Kernel.HasComponent(typeof(TServiceImplementation)) &&
                !Instance.Kernel.HasComponent(name))
            {
                Instance.Register(Component.For(typeof(TServiceImplementation))
                                           .Instance(instance)
                                           .Named(name)
                                           .LifeStyle.Singleton);
            }

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="lifeStyleScopeType"></param>
        /// <typeparam name="TServiceInterface"></typeparam>
        /// <typeparam name="TServiceImplementation"></typeparam>
        /// <returns></returns>
        public IContainerRegistry RegisterTypeWithLifeStyleType<TServiceInterface, TServiceImplementation>(string name, LifestyleType lifeStyleScopeType)
        {
            return RegisterSingleton(typeof(TServiceInterface), typeof(TServiceImplementation), name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceInterfaceType"></param>
        /// <param name="serviceImplementationType"></param>
        /// <returns></returns>
        public IContainerRegistry RegisterSingleton(Type serviceInterfaceType, Type serviceImplementationType)
        {
            if (serviceInterfaceType == null) throw new ArgumentNullException(nameof(serviceInterfaceType));
            if (serviceImplementationType == null) throw new ArgumentNullException(nameof(serviceImplementationType));

            if (!Instance.Kernel.HasComponent(serviceInterfaceType) &&
                !Instance.Kernel.HasComponent(serviceImplementationType) &&
                !Instance.Kernel.HasComponent(serviceImplementationType.FullName))
            {
                Instance.Register(Component.For(serviceInterfaceType)
                    .ImplementedBy(serviceImplementationType)
                    .Named(serviceImplementationType.FullName)
                    .LifeStyle.Singleton);
            }

            return this;
        }

        /// <summary>
        /// Call-down Castle API to Register a Singleton via Typed Object Instance type using a supplied name
        /// </summary>
        /// <param name="serviceInterfaceType"></param>
        /// <param name="serviceImplementationType"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public IContainerRegistry RegisterSingleton(Type serviceInterfaceType, Type serviceImplementationType, string name)
        {
            if (!Instance.IsTypeRegistered(serviceInterfaceType) &&
                !Instance.IsTypeRegistered(serviceImplementationType) &&
                !Instance.Kernel.HasComponent(name))
            {
                Instance.Register(Component.For(serviceInterfaceType)
                    .ImplementedBy(serviceImplementationType)
                    .Named(name)
                    .LifeStyle.Singleton);
            }

            return this;
        }

        /// <summary>
        /// Call-down to the Castle API to Register a Singleton Type via Generics using a supplied name 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IContainerRegistry RegisterSingleton<TServiceInterface, TServiceImplementation>(string name) where TServiceImplementation: class, TServiceInterface
        {
            if (!Instance.IsTypeRegistered<TServiceInterface>() &&
                !Instance.IsTypeRegistered<TServiceImplementation>() &&
                !Instance.Kernel.HasComponent(name))
            {
                Instance.RegisterType<TServiceInterface, TServiceImplementation>(name, LifestyleType.Singleton);
            }

            return this;
        }

        /// <summary>
        /// This method takes no parameters and will register the TInterfaceService and TServiceImplementation using the Fullname of the of the
        /// types in the host assembly
        /// </summary>
        /// <returns></returns>
        public IContainerRegistry RegisterSingleton<TServiceInterface, TServiceImplementation>() where TServiceImplementation : class, TServiceInterface
        {
            // we pass the true value to create a Singleton instance
            Instance.RegisterType<TServiceInterface, TServiceImplementation>(true);

            //if (!Instance.Kernel.HasComponent(typeof(TServiceInterface))      &&
            //    !Instance.Kernel.HasComponent(typeof(TServiceImplementation)) &&
            //    !Instance.Kernel.HasComponent(typeof(TServiceInterface).FullName) &&
            //    !Instance.Kernel.HasComponent(typeof(TServiceImplementation).FullName))
            //{
            //    Instance.Register(Component.For(typeof(TServiceInterface))
            //                               .ImplementedBy(typeof(TServiceImplementation))
            //                               .Named(typeof(TServiceInterface).FullName)
            //                               .LifeStyle.Singleton);
            //}

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromServiceType"></param>
        /// <param name="toServiceType"></param>
        /// <returns></returns>
        public IContainerRegistry Register(Type fromServiceType, Type toServiceType)
        {
            if (!Instance.Kernel.HasComponent(fromServiceType) &&
                !Instance.Kernel.HasComponent(toServiceType.FullName))
            {
                Instance.Register(Component.For(fromServiceType)
                    .ImplementedBy(toServiceType)
                    .Named(toServiceType.FullName)
                    .LifeStyle.Transient);
            }

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromServiceType"></param>
        /// <param name="toServiceType"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public IContainerRegistry Register(Type fromServiceType, Type toServiceType, string name)
        {
            if (!Instance.Kernel.HasComponent(fromServiceType) &&
                !Instance.Kernel.HasComponent(name))
            {
                Instance.Register(Component.For(fromServiceType)
                    .ImplementedBy(toServiceType)
                    .Named(name)
                    .LifeStyle.Transient);
            }

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public object Resolve(Type type)
        {
            if (!Instance.Kernel.HasComponent(type))
                Instance.Register(Component.For(type)
                    .Named(type.FullName)
                    .LifeStyle.Transient);

            return Instance.Resolve(type);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public object Resolve(Type type, string name)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (name == null) throw new ArgumentNullException(nameof(name));
            // Check to see if we can Resolve the type by name

            var typeRegisteredByName = Instance.Kernel.HasComponent(name);
            var typeRegisteredByType = Instance.Kernel.HasComponent(type);

            // Custom Fix-up logic to resolve registration inconsistency 

            if (typeRegisteredByName && !typeRegisteredByType)
            {
                Instance.Register(Component.For(type)
                    .Named(type.FullName)
                    .LifeStyle.Transient);

                return Instance.IsTypeRegistered(type) ? Instance.Resolve(type) : Instance.Resolve(name, type);
            }
            else if (!typeRegisteredByName && typeRegisteredByType)
            {
                Instance.Register(Component.For(type)
                    .Named(name)
                    .LifeStyle.Transient);

                return Instance.Kernel.HasComponent(name) ? Instance.Resolve(type, name) : Instance.Resolve(name, type);
            }
            else if (!typeRegisteredByName && !typeRegisteredByType)
            {
                Instance.Register(Component.For(type)
                    .Named(type.FullName)
                    .LifeStyle.Transient);
                return Instance.Resolve(type);
            }

            return Instance.Resolve(name, type);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public object Resolve(Type type, params (Type Type, object Instance)[] parameters)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            if (parameters.Length == 0)
                throw new ArgumentException($@"Value cannot be an empty collection.", nameof(parameters));

            var overrides = parameters.Select(p => new KeyValuePair<object, object>(p.Type, p.Instance)).ToList();

            var resolveParameters = new Arguments().Add(overrides);

            return Instance.Resolve(type, resolveParameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public object Resolve(Type type, string name, params (Type Type, object Instance)[] parameters)
        {
            var overrides = parameters.Select(p => new KeyValuePair<object, object>(p.Type, p.Instance)).ToList();

            var resolveParameters = new Arguments().Add(overrides);

            return Instance.Resolve(name, type, resolveParameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool IsRegistered(Type type) => Instance.Kernel.HasComponent(type);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsRegistered(Type type, string name) => !Instance.Kernel.HasComponent(type) && !Instance.Kernel.HasComponent(name);
    }
}
