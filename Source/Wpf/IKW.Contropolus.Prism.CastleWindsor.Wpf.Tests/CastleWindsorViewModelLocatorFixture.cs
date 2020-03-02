using Prism.IocContainer.Wpf.Tests.Support.Mocks;
using Prism.IocContainer.Wpf.Tests.Support.Mocks.ViewModels;
using Prism.IocContainer.Wpf.Tests.Support.Mocks.Views;
using Prism.Mvvm;
using Xunit;

namespace IKW.Contropolus.Prism.CastleWindsor.Wpf.Tests
{
    using WPF.Legacy;

    [Collection("ServiceLocator")]
    public class CastleWindsorViewModelLocatorFixture
    {
        [StaFact]
        public void ShouldLocateViewModelAndResolveWithContainer()
        {
            var bootstrapper = new DefaultUnityBootstrapper();
            bootstrapper.Run();

            bootstrapper.BaseContainer.RegisterType<IService, MockService>();

            MockView view = new MockView();
            Assert.Null(view.DataContext);

            ViewModelLocator.SetAutoWireViewModel(view, true);
            Assert.NotNull(view.DataContext);
            Assert.IsType<MockViewModel>(view.DataContext);

            Assert.NotNull(((MockViewModel)view.DataContext).MockService);
        }
    }
}
