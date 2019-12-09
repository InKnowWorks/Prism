using System.Windows;
using Castle.MicroKernel.Registration;
using CommonServiceLocator;
using IKW.Contropolus.Prism.CastleWindsor.WPF.Legacy;
using IKW.Contropolus.Prism.CastleWindsor.Wpf.ServiceLocator;
using IKW.Contropolus.Prism.CastleWindsor.WPF.Ioc;
using Prism.Ioc;
using Prism.Logging;
using Prism.Modularity;
using Prism.Regions;
using Xunit;

namespace IKW.Contropolus.Prism.CastleWindsor.Wpf.Tests
{
    
    public class CastleWindsorBootstrapperNullModuleManagerFixture
    {
        [Fact]
        public void RunShouldNotCallInitializeModulesWhenModuleManagerNotFound()
        {
            var bootstrapper = new NullModuleManagerBootstrapper();

            bootstrapper.Run();

            Assert.False(bootstrapper.InitializeModulesCalled);
        }

        private class NullModuleManagerBootstrapper : CastleWindsorBootstrapper
        {
            public bool InitializeModulesCalled;

            protected override void ConfigureContainer()
            {
                //base.RegisterDefaultTypesIfMissing();

                Container.Register(Component.For(typeof(ILoggerFacade))
                    .Instance(Logger)
                    .LifeStyle.Singleton);

                //Container.RegisterType<ILoggerFacade>("Logger", Castle.Core.LifestyleType.Singleton);

                Container.Register(Component.For(typeof(ModuleCatalog))
                    .Instance(ModuleCatalog)
                    .LifeStyle.Singleton);

                //this.Container.RegisterInstance(this.ModuleCatalog);

                RegisterTypeIfMissing(typeof(IServiceLocator), typeof(CastleWindsorServiceLocatorAdapter), true);
            }

            protected override IRegionBehaviorFactory ConfigureDefaultRegionBehaviors()
            {
                return null;
            }

            protected override RegionAdapterMappings ConfigureRegionAdapterMappings()
            {
                return null;
            }

            protected override DependencyObject CreateShell()
            {
                return null;
            }

            protected override IContainerExtension CreateContainerExtension()
            {
                throw new System.NotImplementedException();
            }

            protected override void InitializeModules()
            {
                this.InitializeModulesCalled = true;
            }
        }
    }
}
