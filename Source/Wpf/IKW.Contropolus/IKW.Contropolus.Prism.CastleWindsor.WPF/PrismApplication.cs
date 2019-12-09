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
    public abstract class PrismApplication : PrismApplicationBase
    {
        protected override IContainerExtension CreateContainerExtension()
        {
            return new CastleWindsorContainerExtension();
        }

        protected override void RegisterRequiredTypes(IContainerRegistry containerRegistry)
        {
            base.RegisterRequiredTypes(containerRegistry);
            containerRegistry.RegisterSingleton<IRegionNavigationContentLoader, CastleWindsorRegionNavigationContentLoader>();
            containerRegistry.RegisterSingleton<IServiceLocator, CastleWindsorServiceLocatorAdapter>();
        }

        protected override void RegisterFrameworkExceptionTypes()
        {
            base.RegisterFrameworkExceptionTypes();
            ExceptionExtensions.RegisterFrameworkExceptionType(typeof(ResolutionFailedException));
        }
    }
}
