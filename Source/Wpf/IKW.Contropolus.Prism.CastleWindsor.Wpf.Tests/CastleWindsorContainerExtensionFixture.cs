using System.Collections.Generic;
using Prism.IocContainer.Wpf.Tests.Support.Mocks;
using IKW.Contropolus.Prism.CastleWindsor.WPF.Ioc;
using Xunit;

namespace IKW.Contropolus.Prism.CastleWindsor.Wpf.Tests
{
    using System.Collections;
    using Castle.Windsor;
    using WPF.Ioc;
    using WPF.Legacy;

    public class CastleWindsorContainerExtensionFixture
    {
        [Fact]
        public void ExtensionReturnsTrueIfThereIsAPolicyForType()
        {
            WindsorContainer container = new WindsorContainer();

            container.RegisterType<object, string>();
            Assert.True(container.IsTypeRegistered(typeof(object)));
            Assert.False(container.IsTypeRegistered(typeof(int)));

            container.RegisterType<IList<int>, List<int>>();

            Assert.True(container.IsTypeRegistered(typeof(IList<int>)));
            Assert.False(container.IsTypeRegistered(typeof(IList<string>)));

            //container.Register(typeof(IDictionary<,>), typeof(Dictionary<,>));
            //Assert.True(container.IsTypeRegistered(typeof(IDictionary<,>)));
        }

        [Fact]
        public void TryResolveShouldResolveTheElementIfElementExist()
        {
            var container = new WindsorContainer();
            container.RegisterType<IService, MockService>();

            object dependantA = container.TryResolve<IService>();
            Assert.NotNull(dependantA);
        }

        [Fact]
        public void TryResolveShouldReturnNullIfElementNotExist()
        {
            var container = new WindsorContainer();

            object dependantA = container.TryResolve<IDependantA>();
            Assert.Null(dependantA);
        }

        [Fact]
        public void TryResolveWorksWithValueTypes()
        {
            var container = new WindsorContainer();

            int valueType = container.TryResolve<int>();
            Assert.Equal(default(int), valueType);
        }

    }
}