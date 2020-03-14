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
        public IWindsorContainer Instance { get; }

        /// <summary>
        /// 
        /// </summary>
        public CastleWindsorContainerExtension() : this(new WindsorContainer()) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        public CastleWindsorContainerExtension(IWindsorContainer container)
        {
            Instance = container;

            Instance.Register(Component.For<IWindsorContainer>().Instance(container));

            // register region adapters
            Instance.Register(Classes.FromAssemblyContaining<IRegionAdapter>().BasedOn<IRegionAdapter>().LifestyleTransient());

            // register region behaviors
            Instance.Register(Classes.FromAssemblyContaining<IRegionBehavior>().BasedOn<IRegionBehavior>().LifestyleTransient());

            Instance.RegisterType<DelayedRegionCreationBehavior,DelayedRegionCreationBehavior>(false);
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
        /// 
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="instance"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public IContainerRegistry RegisterSingletonType<TService>(object instance, string name)
        {
            if (!Instance.Kernel.HasComponent(typeof(TService)) &&
                !Instance.Kernel.HasComponent(name))
            {
                Instance.Register(Component.For(typeof(TService))
                                           .Instance(instance)
                                           .Named(name)
                                           .LifeStyle.Singleton);
            }

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public IContainerRegistry RegisterSingleton(Type from, Type to)
        {
            if (@from == null) throw new ArgumentNullException(nameof(@from));
            if (to == null) throw new ArgumentNullException(nameof(to));

            if (!Instance.Kernel.HasComponent(from) &&
                !Instance.Kernel.HasComponent(to.FullName))
            {
                Instance.Register(Component.For(from)
                    .ImplementedBy(to)
                    .Named(to.FullName)
                    .LifeStyle.Singleton);
            }

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public IContainerRegistry RegisterSingleton(Type from, Type to, string name)
        {
            if (!Instance.Kernel.HasComponent(from) &&
                !Instance.Kernel.HasComponent(name))
            {
                Instance.Register(Component.For(from)
                    .ImplementedBy(to)
                    .Named(name)
                    .LifeStyle.Singleton);
            }

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IContainerRegistry RegisterSingleton<TInterface, TImplementation>(string name) where TImplementation: class
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("message", nameof(name));
            }

            if (!Instance.Kernel.HasComponent(typeof(TInterface)) &&
                !Instance.Kernel.HasComponent(typeof(TImplementation)) &&
                !Instance.Kernel.HasComponent(name))
            {
                Instance.Register(Component.For(typeof(TInterface))
                                           .ImplementedBy(typeof(TImplementation))
                                           .Named(name)
                                           .LifeStyle.Singleton);
            }

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IContainerRegistry RegisterSingleton<TInterface, TImplementation>() where TImplementation : class
        {
            if (!Instance.Kernel.HasComponent(typeof(TInterface))      &&
                !Instance.Kernel.HasComponent(typeof(TImplementation)) &&
                !Instance.Kernel.HasComponent(typeof(TInterface).FullName) &&
                !Instance.Kernel.HasComponent(typeof(TImplementation).FullName))
            {
                Instance.Register(Component.For(typeof(TInterface))
                                           .ImplementedBy(typeof(TImplementation))
                                           .Named(typeof(TInterface).FullName)
                                           .LifeStyle.Singleton);
            }

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public IContainerRegistry Register(Type from, Type to)
        {
            if (!Instance.Kernel.HasComponent(from) &&
                !Instance.Kernel.HasComponent(to.FullName))
            {
                Instance.Register(Component.For(from)
                    .ImplementedBy(to)
                    .Named(to.FullName)
                    .LifeStyle.Transient);
            }

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public IContainerRegistry Register(Type from, Type to, string name)
        {
            if (!Instance.Kernel.HasComponent(from) &&
                !Instance.Kernel.HasComponent(name))
            {
                Instance.Register(Component.For(from)
                    .ImplementedBy(to)
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
            if (type.IsClass && !Instance.Kernel.HasComponent(type))
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
            if (type.IsClass && !Instance.Kernel.HasComponent(type))
                Instance.Register(Component.For(type)
                    .Named(name)
                    .LifeStyle.Transient);

            return Instance.Resolve(type, name);
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
