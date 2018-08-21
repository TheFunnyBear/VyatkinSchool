using Unity;
using VyatkinSchool.Tests.Steps;
using VyatkinSchool.Tests.Constants;
using Unity.Lifetime;

namespace VyatkinSchool.Tests
{
    public abstract class UnitTestBase
    {
        public IUnityContainer Container { get; set; }

        public UnitTestBase()
        {
            RegisterUnity();
        }

        public virtual void TestInitialize()
        {
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
        }

        public virtual void TestCleanup()
        {

        }

        private void RegisterUnity()
        {
            Container = new UnityContainer();

            Container.RegisterType<IBrowserNavigator, BrowserNavigator>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IButtonCliker, ButtonCliker>(new ContainerControlledLifetimeManager());
            Container.RegisterType<ILinkCliker, LinkCliker>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IHeaderButtonChecker, HeaderButtonChecker>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IHeaderChecker, HeaderChecker>(new ContainerControlledLifetimeManager());
            Container.RegisterType<ITestsConstants, TestsConstants>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IOpenBrowserSteps, OpenBrowserSteps>(new ContainerControlledLifetimeManager());
            Container.RegisterType<INewsPageSteps, NewsPageSteps>(new ContainerControlledLifetimeManager());
            Container.RegisterType<ILoginPageSteps, LoginPageSteps>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IMessageCreatedSteps, MessageCreatedSteps>(new ContainerControlledLifetimeManager());
            Container.RegisterType<INavigationBarSteps, NavigationBarSteps>(new ContainerControlledLifetimeManager());
        }
    }
}
