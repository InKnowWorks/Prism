using System;
using System.Linq;
using Prism.Ioc;
using Unity;
using Unity.Resolution;

namespace Prism.Unity.Ioc
{
    public class UnityContainerExtension : IContainerExtension<IUnityContainer>
    {
        public IUnityContainer Instance { get; }

        public UnityContainerExtension() : this(new UnityContainer()) { }

        public UnityContainerExtension(IUnityContainer container) => Instance = container;

        public void FinalizeExtension() { }

        public IContainerRegistry RegisterInstance(Type type, object instance)
        {
            Instance.RegisterInstance(type, instance);
            return this;
        }

        public IContainerRegistry RegisterInstance(Type type, object instance, string name)
        {
            Instance.RegisterInstance(type, name, instance);
            return this;
        }

        public IContainerRegistry RegisterSingleton(Type serviceInterfaceType, Type serviceImplementationType)
        {
            Instance.RegisterSingleton(serviceInterfaceType, serviceImplementationType);
            return this;
        }

        public IContainerRegistry RegisterSingleton(Type serviceInterfaceType, Type serviceImplementationType, string name)
        {
            Instance.RegisterSingleton(serviceInterfaceType, serviceImplementationType, name);
            return this;
        }

        public IContainerRegistry Register(Type fromServiceType, Type toServiceType)
        {
            Instance.RegisterType(fromServiceType, toServiceType);
            return this;
        }

        public IContainerRegistry Register(Type fromServiceType, Type toServiceType, string name)
        {
            Instance.RegisterType(fromServiceType, toServiceType, name);
            return this;
        }

        public object Resolve(Type type)
        {
            return Instance.Resolve(type);
        }

        public object Resolve(Type type, string name)
        {
            return Instance.Resolve(type, name);
        }

        public object Resolve(Type type, params (Type Type, object Instance)[] parameters)
        {
            var overrides = parameters.Select(p => new DependencyOverride(p.Type, p.Instance)).ToArray();
            return Instance.Resolve(type, overrides);
        }

        public object Resolve(Type type, string name, params (Type Type, object Instance)[] parameters)
        {
            var overrides = parameters.Select(p => new DependencyOverride(p.Type, p.Instance)).ToArray();
            return Instance.Resolve(type, name, overrides);
        }

        public bool IsRegistered(Type type)
        {
            return Instance.IsRegistered(type);
        }

        public bool IsRegistered(Type type, string name)
        {
            return Instance.IsRegistered(type, name);
        }
    }
}
