using Microsoft.Practices.Unity;
using VyatkinSchool.Tests.Steps;
using VyatkinSchool.Tests.Constants;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VyatkinSchool.Tests
{
    [TestClass]
    public sealed class NaviBarTests : UnitTestBase
    {
        [Dependency]
        public ITestsConstants TestsConstants { get; set; }

        [Dependency]
        public IOpenBrowserSteps OpenBrowserSteps { get; set; }

        [Dependency]
        public INewsPageSteps NewsPageSteps { get; set; }

        [Dependency]
        public ILoginPageSteps LoginPageSteps { get; set; }

        [Dependency]
        public INavigationBarSteps NavigationBarSteps { get; set; }

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            TestsConstants = Container.Resolve<ITestsConstants>();
            OpenBrowserSteps = Container.Resolve<IOpenBrowserSteps>();
            NewsPageSteps = Container.Resolve<INewsPageSteps>();
            LoginPageSteps = Container.Resolve<ILoginPageSteps>();
            NavigationBarSteps = Container.Resolve<INavigationBarSteps>();
        }

        [TestCleanup]
        public override void TestCleanup()
        {
            OpenBrowserSteps.CloseBrowser();
        }

        [TestMethod]
        public void NewsPageShown()
        {
            OpenBrowserSteps.OpenBrowser();
            OpenBrowserSteps.Navigate(TestsConstants.WebSiteHttpAddress);
            OpenBrowserSteps.CheckThatMainPageOpnned();

            NavigationBarSteps.ClickOnGalleryHeader();
            NavigationBarSteps.CheckThatGalleryPageOpnned();

            NavigationBarSteps.ClickOnNewsHeader();
            NavigationBarSteps.CheckThatNewsPageOpnned();
        }

        [TestMethod]
        public void GaleryPageShown()
        {
            OpenBrowserSteps.OpenBrowser();
            OpenBrowserSteps.Navigate(TestsConstants.WebSiteHttpAddress);
            OpenBrowserSteps.CheckThatMainPageOpnned();

            NavigationBarSteps.ClickOnGalleryHeader();
            NavigationBarSteps.CheckThatGalleryPageOpnned();
        }

        [TestMethod]
        public void TimeTablePageShown()
        {
            OpenBrowserSteps.OpenBrowser();
            OpenBrowserSteps.Navigate(TestsConstants.WebSiteHttpAddress);
            OpenBrowserSteps.CheckThatMainPageOpnned();

            NavigationBarSteps.ClickOnTimeTableHeader();
            NavigationBarSteps.CheckThatTimeTablePageOpnned();
        }

        [TestMethod]
        public void ContactsPageShown()
        {
            OpenBrowserSteps.OpenBrowser();
            OpenBrowserSteps.Navigate(TestsConstants.WebSiteHttpAddress);
            OpenBrowserSteps.CheckThatMainPageOpnned();

            NavigationBarSteps.ClickOnContactsHeader();
            NavigationBarSteps.CheckThatContactsageOpnned();
        }

        [TestMethod]
        public void LoginPageShown()
        {
            OpenBrowserSteps.OpenBrowser();
            OpenBrowserSteps.Navigate(TestsConstants.WebSiteHttpAddress);
            OpenBrowserSteps.CheckThatMainPageOpnned();

            NavigationBarSteps.ClickOnLoginHeader();
            NavigationBarSteps.CheckThatLoginOpnned();
        }

        [TestMethod]
        public void NewsPageActive()
        {
            OpenBrowserSteps.OpenBrowser();
            OpenBrowserSteps.Navigate(TestsConstants.WebSiteHttpAddress);
            OpenBrowserSteps.CheckThatMainPageOpnned();
            NavigationBarSteps.CheckThatNewsPageActive();
        }

        [TestMethod]
        public void GaleryPageActive()
        {
            OpenBrowserSteps.OpenBrowser();
            OpenBrowserSteps.Navigate(TestsConstants.WebSiteHttpAddress);
            OpenBrowserSteps.CheckThatMainPageOpnned();

            NavigationBarSteps.ClickOnGalleryHeader();
            NavigationBarSteps.CheckThatGalleryPageActive();
        }

        [TestMethod]
        public void TimeTablePageActive()
        {
            OpenBrowserSteps.OpenBrowser();
            OpenBrowserSteps.Navigate(TestsConstants.WebSiteHttpAddress);
            OpenBrowserSteps.CheckThatMainPageOpnned();

            NavigationBarSteps.ClickOnTimeTableHeader();
            NavigationBarSteps.CheckThatTimeTablePageActive();
        }

        [TestMethod]
        public void ContactsPageActive()
        {
            OpenBrowserSteps.OpenBrowser();
            OpenBrowserSteps.Navigate(TestsConstants.WebSiteHttpAddress);
            OpenBrowserSteps.CheckThatMainPageOpnned();

            NavigationBarSteps.ClickOnContactsHeader();
            NavigationBarSteps.CheckThatContactsButtonActive();
        }

        [TestMethod]
        public void LoginPagActive()
        {
            OpenBrowserSteps.OpenBrowser();
            OpenBrowserSteps.Navigate(TestsConstants.WebSiteHttpAddress);
            OpenBrowserSteps.CheckThatMainPageOpnned();

            NavigationBarSteps.ClickOnLoginHeader();
            NavigationBarSteps.CheckThatLoginActive();
        }

    }
}
