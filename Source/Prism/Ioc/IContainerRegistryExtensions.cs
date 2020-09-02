using System;

namespace Prism.Ioc
{
    /// <summary>
    /// 
    /// </summary>
    public static class IContainerRegistryExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="containerRegistry"></param>
        /// <param name="instance"></param>
        /// <typeparam name="TInterface"></typeparam>
        /// <returns></returns>
        public static IContainerRegistry RegisterInstance<TInterface>(this IContainerRegistry containerRegistry, TInterface instance)
        {
            return containerRegistry.RegisterInstance(typeof(TInterface), instance);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="containerRegistry"></param>
        /// <param name="instance"></param>
        /// <param name="name"></param>
        /// <typeparam name="TInterface"></typeparam>
        /// <returns></returns>
        public static IContainerRegistry RegisterInstance<TInterface>(this IContainerRegistry containerRegistry, TInterface instance, string name)
        {
            return containerRegistry.RegisterInstance(typeof(TInterface), instance, name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="containerRegistry"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IContainerRegistry RegisterSingleton(this IContainerRegistry containerRegistry, Type type)
        {
            return containerRegistry.RegisterSingleton(type, type);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="containerRegistry"></param>
        /// <typeparam name="TFrom"></typeparam>
        /// <typeparam name="TTo"></typeparam>
        /// <returns></returns>
        public static IContainerRegistry RegisterSingleton<TFrom, TTo>(this IContainerRegistry containerRegistry) where TTo : TFrom
        {
            return containerRegistry.RegisterSingleton(typeof(TFrom), typeof(TTo));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="containerRegistry"></param>
        /// <param name="containerExtension"></param>
        /// <param name="scopeAsSingleton"></param>
        /// <typeparam name="TInterface"></typeparam>
        /// <returns></returns>
        public static IContainerRegistry RegisterInstance<TInterface>(this IContainerRegistry containerRegistry, TInterface containerExtension, bool scopeAsSingleton)
        {
            return containerRegistry.RegisterSingleton(typeof(TInterface));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="containerRegistry"></param>
        /// <param name="name"></param>
        /// <typeparam name="TFrom"></typeparam>
        /// <typeparam name="TTo"></typeparam>
        /// <returns></returns>
        public static IContainerRegistry RegisterSingleton<TFrom, TTo>(this IContainerRegistry containerRegistry, string name) where TTo : TFrom
        {
            return containerRegistry.RegisterSingleton(typeof(TFrom), typeof(TTo), name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="containerRegistry"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IContainerRegistry RegisterSingleton<T>(this IContainerRegistry containerRegistry)
        {
            return containerRegistry.RegisterSingleton(typeof(T));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="containerRegistry"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IContainerRegistry Register(this IContainerRegistry containerRegistry, Type type)
        {
            return containerRegistry.Register(type, type);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="containerRegistry"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IContainerRegistry Register<T>(this IContainerRegistry containerRegistry)
        {
            return containerRegistry.Register(typeof(T));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="containerRegistry"></param>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IContainerRegistry Register(this IContainerRegistry containerRegistry, Type type, string name)
        {
            return containerRegistry.Register(type, type, name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="containerRegistry"></param>
        /// <param name="name"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IContainerRegistry Register<T>(this IContainerRegistry containerRegistry, string name)
        {
            return containerRegistry.Register(typeof(T), name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="containerRegistry"></param>
        /// <typeparam name="TFrom"></typeparam>
        /// <typeparam name="TTo"></typeparam>
        /// <returns></returns>
        public static IContainerRegistry Register<TFrom, TTo>(this IContainerRegistry containerRegistry) where TTo : TFrom
        {
            return containerRegistry.Register(typeof(TFrom), typeof(TTo));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="containerRegistry"></param>
        /// <param name="name"></param>
        /// <typeparam name="TFrom"></typeparam>
        /// <typeparam name="TTo"></typeparam>
        /// <returns></returns>
        public static IContainerRegistry Register<TFrom, TTo>(this IContainerRegistry containerRegistry, string name) where TTo : TFrom
        {
            return containerRegistry.Register(typeof(TFrom), typeof(TTo), name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="containerRegistry"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool IsRegistered<T>(this IContainerRegistry containerRegistry)
        {
            return containerRegistry.IsRegistered(typeof(T));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="containerRegistry"></param>
        /// <param name="name"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool IsRegistered<T>(this IContainerRegistry containerRegistry, string name)
        {
            return containerRegistry.IsRegistered(typeof(T), name);
        }
    }
}
