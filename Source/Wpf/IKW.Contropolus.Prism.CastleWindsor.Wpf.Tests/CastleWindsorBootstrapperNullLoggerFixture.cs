using System;
using System.Windows;
using IKW.Contropolus.Prism.CastleWindsor.WPF.Legacy;
using Prism.IocContainer.Wpf.Tests.Support;
using Prism.Logging;
using Xunit;

namespace IKW.Contropolus.Prism.CastleWindsor.Wpf.Tests
{
    using global::Prism.Ioc;

    public class CastleWindsorBootstrapperNullLoggerFixture : BootstrapperFixtureBase
    {
        [Fact]
        public void NullLoggerThrows()
        {
            var bootstrapper = new NullLoggerBootstrapper();

            AssertExceptionThrownOnRun(bootstrapper, typeof(InvalidOperationException), "ILoggerFacade");
        }

        internal class NullLoggerBootstrapper : CastleWindsorBootstrapper
        {
            protected override IContainerExtension CreateContainerExtension()
            {
                throw new NotImplementedException();
            }

            protected override ILoggerFacade CreateLogger()
            {
                return null;
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