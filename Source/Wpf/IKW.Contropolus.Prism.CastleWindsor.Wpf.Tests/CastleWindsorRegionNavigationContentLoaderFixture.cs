using System.Linq;
using CommonServiceLocator;
using IKW.Contropolus.Prism.CastleWindsor.Wpf.Tests.Mocks;
using Prism.IocContainer.Wpf.Tests.Support.Mocks.Views;
using Prism.Regions;
using Xunit;

namespace IKW.Contropolus.Prism.CastleWindsor.Wpf.Tests
{
    using Castle.Windsor;
    using WPF.Legacy;

    [Collection("ServiceLocator")]
    public class CastleWindsorRegionNavigationContentLoaderFixture
    {
        IWindsorContainer _container;

        public CastleWindsorRegionNavigationContentLoaderFixture()
        {
            _container = new WindsorContainer();
            MockServiceLocator serviceLocator = new MockServiceLocator(_container);
        }

        [StaFact]
        public void ShouldFindCandidateViewInRegion()
        {
            _container.RegisterType<object, MockView>("MockView");

            // We cannot access the UnityRegionNavigationContentLoader directly so we need to call its
            // GetCandidatesFromRegion method through a navigation request.
            IRegion testRegion = new Region();

            MockView view = new MockView();
            testRegion.Add(view);
            testRegion.Deactivate(view);

            testRegion.RequestNavigate("MockView");

            Assert.True(testRegion.Views.Contains(view));
            Assert.True(testRegion.Views.Count() == 1);
            Assert.True(testRegion.ActiveViews.Count() == 1);
            Assert.True(testRegion.ActiveViews.Contains(view));
        }

        [StaFact]
        public void ShouldFindCandidateViewWithFriendlyNameInRegion()
        {
            _container.RegisterType<object, MockView>("SomeView");

            // We cannot access the UnityRegionNavigationContentLoader directly so we need to call its
            // GetCandidatesFromRegion method through a navigation request.
            IRegion testRegion = new Region();

            MockView view = new MockView();
            testRegion.Add(view);
            testRegion.Deactivate(view);

            testRegion.RequestNavigate("SomeView");

            Assert.True(testRegion.Views.Contains(view));
            Assert.True(testRegion.ActiveViews.Count() == 1);
            Assert.True(testRegion.ActiveViews.Contains(view));
        }
    }
}
