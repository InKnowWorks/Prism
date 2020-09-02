using System;
using System.Linq;
using DryIoc;
using Prism.Ioc;

namespace Prism.DryIoc.Ioc
{
    public class DryIocContainerExtension : IContainerExtension<IContainer>
    {
        public IContainer Instance { get; }

        public DryIocContainerExtension(IContainer container)
        {
            Instance = container;
        }

        public void FinalizeExtension() { }

        public IContainerRegistry RegisterInstance(Type type, object instance)
        {
            Instance.UseInstance(type, instance);
            return this;
        }

        public IContainerRegistry RegisterInstance(Type type, object instance, string name)
        {
            Instance.UseInstance(type, instance, serviceKey: name);
            return this;
        }

        public IContainerRegistry RegisterSingleton(Type serviceInterfaceType, Type serviceImplementationType)
        {
            Instance.Register(serviceInterfaceType, serviceImplementationType, Reuse.Singleton);
            return this;
        }

        public IContainerRegistry RegisterSingleton(Type serviceInterfaceType, Type serviceImplementationType, string name)
        {
            Instance.Register(serviceInterfaceType, serviceImplementationType, Reuse.Singleton, serviceKey: name);
            return this;
        }

        public IContainerRegistry Register(Type fromServiceType, Type toServiceType)
        {
            Instance.Register(fromServiceType, toServiceType);
            return this;
        }

        public IContainerRegistry Register(Type fromServiceType, Type toServiceType, string name)
        {
            Instance.Register(fromServiceType, toServiceType, serviceKey: name);
            return this;
        }

        public object Resolve(Type type)
        {
            return Instance.Resolve(type);
        }

        public object Resolve(Type type, string name)
        {
            return Instance.Resolve(type, serviceKey: name);
        }

        public object Resolve(Type type, params (Type Type, object Instance)[] parameters)
        {
            return Instance.Resolve(type, args: parameters.Select(p => p.Instance).ToArray());
        }

        public object Resolve(Type type, string name, params (Type Type, object Instance)[] parameters)
        {
            return Instance.Resolve(type, name, args: parameters.Select(p => p.Instance).ToArray());
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
