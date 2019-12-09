using System;
using System.Windows;
using Castle.Windsor;
using IKW.Contropolus.Prism.CastleWindsor.WPF.Legacy;
using Prism.Ioc;
using Prism.IocContainer.Wpf.Tests.Support;
using Xunit;

namespace IKW.Contropolus.Prism.CastleWindsor.Wpf.Tests
{
    
    public class CastleWindsorBootstrapperNullContainerFixture : BootstrapperFixtureBase
    {
        [Fact]
        public void RunThrowsWhenNullContainerCreated()
        {
            var bootstrapper = new NullContainerBootstrapper();

            AssertExceptionThrownOnRun(bootstrapper, typeof(InvalidOperationException), "IWindsorContainer");
        }

        private class NullContainerBootstrapper : CastleWindsorBootstrapper
        {
            protected override IWindsorContainer CreateContainer()
            {
                return null;
            }

            protected override IContainerExtension CreateContainerExtension()
            {
                throw new NotImplementedException();
            }

            protected override DependencyObject CreateShell()
            {
                throw new NotImplementedException();
            }

            protected override void InitializeShell()
            {
                throw new NotImplementedException();
            }
        }    
    }
}