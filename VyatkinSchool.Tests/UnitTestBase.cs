using Microsoft.Practices.Unity;
using VyatkinSchool.Tests.Steps;
using VyatkinSchool.Tests.Constants;

namespace VyatkinSchool.Tests
{

    public abstract class UnitTestBase
    {

        static public IUnityContainer Container { get; set; }

        public UnitTestBase()
        {
        }

        public virtual void TestInitialize()
        {
            Container = TestDependencies.CreateContainer();
            Container.Resolve<BrowserNavigator>();
            Container.Resolve<ButtonCliker>();
            Container.Resolve<LinkCliker>();
            Container.Resolve<HeaderButtonChecker>();
            Container.Resolve<HeaderChecker>();
            Container.Resolve<TestsConstants>();
            Container.Resolve<OpenBrowserSteps>();
            Container.Resolve<NewsPageSteps>();
            Container.Resolve<LoginPageSteps>();
            Container.Resolve<MessageCreatedSteps>();
            Container.Resolve<NavigationBarSteps>();
            Container.Resolve<GallerySteps>();
        }

        public virtual void TestCleanup()
        {

        }

    }
}
