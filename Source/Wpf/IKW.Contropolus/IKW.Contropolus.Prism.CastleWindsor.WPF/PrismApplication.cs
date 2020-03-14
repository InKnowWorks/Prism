using System;
using CommonServiceLocator;
using IKW.Contropolus.Prism.CastleWindsor.WPF.ExceptionResolution;
using IKW.Contropolus.Prism.CastleWindsor.WPF.Ioc;
using IKW.Contropolus.Prism.CastleWindsor.Wpf.Regions;
using IKW.Contropolus.Prism.CastleWindsor.Wpf.ServiceLocator;
using Prism;
using Prism.Ioc;
using Prism.Regions;

namespace IKW.Contropolus.Prism.CastleWindsor.WPF
{
    using Castle.MicroKernel;
    using Castle.Windsor.Configuration.Interpreters;

    /// <summary>
    /// 
    /// </summary>
    public abstract class PrismApplication : PrismApplicationBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override IContainerExtension CreateContainerExtension()
        {
            return new CastleWindsorContainerExtension();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="containerRegistry"></param>
        protected override void RegisterRequiredTypes(IContainerRegistry containerRegistry)
        {
            base.RegisterRequiredTypes(containerRegistry);

            containerRegistry.RegisterSingleton<IRegionNavigationContentLoader, CastleWindsorRegionNavigationContentLoader>();
            containerRegistry.RegisterSingleton<IRegionNavigationContentLoader, RegionNavigationContentLoader>();
            containerRegistry.RegisterSingleton<IServiceLocator, CastleWindsorServiceLocatorAdapter>();
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void RegisterFrameworkExceptionTypes()
        {
            base.RegisterFrameworkExceptionTypes();

            ExceptionExtensions.RegisterFrameworkExceptionType(typeof(ResolutionFailedException));
            ExceptionExtensions.RegisterFrameworkExceptionType(typeof(ConfigurationProcessingException));
            ExceptionExtensions.RegisterFrameworkExceptionType(typeof(ComponentResolutionException));
            ExceptionExtensions.RegisterFrameworkExceptionType(typeof(ComponentNotFoundException));
            ExceptionExtensions.RegisterFrameworkExceptionType(typeof(ComponentRegistrationException));
            ExceptionExtensions.RegisterFrameworkExceptionType(typeof(CircularDependencyException));
        }
    }
}
