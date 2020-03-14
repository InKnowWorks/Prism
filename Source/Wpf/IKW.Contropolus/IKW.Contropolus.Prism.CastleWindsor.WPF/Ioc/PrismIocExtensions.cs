using Castle.Windsor;
using Prism.Ioc;

namespace IKW.Contropolus.Prism.CastleWindsor.WPF.Ioc
{
    /// <summary>
    /// 
    /// </summary>
    public static class PrismIocExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="containerProvider"></param>
        /// <returns></returns>
        public static IWindsorContainer GetContainer(this IContainerProvider containerProvider)
        {
            return ((IContainerExtension<IWindsorContainer>)containerProvider).Instance;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="containerRegistry"></param>
        /// <returns></returns>
        public static IWindsorContainer GetContainer(this IContainerRegistry containerRegistry)
        {
            return ((IContainerExtension<IWindsorContainer>)containerRegistry).Instance;
        }
    }
}