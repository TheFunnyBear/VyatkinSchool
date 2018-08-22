using VyatkinSchool.Tests.Steps;
using VyatkinSchool.Tests.Constants;
using Microsoft.Practices.Unity;

namespace VyatkinSchool.Tests
{
    public static class Dependencies
    {
        public static IUnityContainer CreateContainer()
        {
            var container = new UnityContainer();

            container.RegisterType<IBrowserNavigator, BrowserNavigator>(new ContainerControlledLifetimeManager());
            container.RegisterType<IButtonCliker, ButtonCliker>(new ContainerControlledLifetimeManager());
            container.RegisterType<ILinkCliker, LinkCliker>(new ContainerControlledLifetimeManager());
            container.RegisterType<IHeaderButtonChecker, HeaderButtonChecker>(new ContainerControlledLifetimeManager());
            container.RegisterType<IHeaderChecker, HeaderChecker>(new ContainerControlledLifetimeManager());
            container.RegisterType<ITestsConstants, TestsConstants>(new ContainerControlledLifetimeManager());
            container.RegisterType<IOpenBrowserSteps, OpenBrowserSteps>(new ContainerControlledLifetimeManager());
            container.RegisterType<INewsPageSteps, NewsPageSteps>(new ContainerControlledLifetimeManager());
            container.RegisterType<ILoginPageSteps, LoginPageSteps>(new ContainerControlledLifetimeManager());
            container.RegisterType<IMessageCreatedSteps, MessageCreatedSteps>(new ContainerControlledLifetimeManager());
            container.RegisterType<INavigationBarSteps, NavigationBarSteps>(new ContainerControlledLifetimeManager());

            return container;
        }
    }
}
