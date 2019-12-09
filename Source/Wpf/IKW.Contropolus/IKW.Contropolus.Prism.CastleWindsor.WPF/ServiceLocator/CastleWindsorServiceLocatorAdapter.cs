using System;
using System.Collections.Generic;
using Castle.Windsor;
using CommonServiceLocator;
using IKW.Contropolus.Prism.CastleWindsor.WPF.Ioc;
using IKW.Contropolus.Prism.CastleWindsor.WPF.Legacy;

namespace IKW.Contropolus.Prism.CastleWindsor.Wpf.ServiceLocator
{
    /// <summary>
    /// 
    /// </summary>
    public class CastleWindsorServiceLocatorAdapter : ServiceLocatorImplBase
    {
        private readonly IWindsorContainer _windsorContainer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="theWindsorContainer"></param>
        public CastleWindsorServiceLocatorAdapter(IWindsorContainer theWindsorContainer)
        {
            _windsorContainer = theWindsorContainer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        protected override object DoGetInstance(Type serviceType, string key)
        {
            return CastleWindsorContainerExtensions.Resolve(_windsorContainer, serviceType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            var enumerable = _windsorContainer.ResolveAll(serviceType) as IEnumerable<object>;
            return enumerable;
        }
    }
}