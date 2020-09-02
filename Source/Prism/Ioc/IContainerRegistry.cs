using System;

namespace Prism.Ioc
{
    public interface IContainerRegistry
    {
        IContainerRegistry RegisterInstance(Type type, object instance);

        IContainerRegistry RegisterInstance(Type type, object instance, string name);

        IContainerRegistry RegisterSingleton(Type serviceInterfaceType, Type serviceImplementationType);

        IContainerRegistry RegisterSingleton(Type serviceInterfaceType, Type serviceImplementationType, string name);

        IContainerRegistry Register(Type fromServiceType, Type toServiceType);

        IContainerRegistry Register(Type fromServiceType, Type toServiceType, string name);

        bool IsRegistered(Type type);

        bool IsRegistered(Type type, string name);
    }
}
