using Microsoft.Practices.Unity;
using VyatkinSchool.Tests.Steps;
using VyatkinSchool.Tests.Constants;
using NUnit.Framework;

namespace VyatkinSchool.Tests
{
    [TestFixture]
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

        [SetUp]
        public override void TestInitialize()
        {
            base.TestInitialize();
            TestsConstants = Container.Resolve<ITestsConstants>();
            OpenBrowserSteps = Container.Resolve<IOpenBrowserSteps>();
            NewsPageSteps = Container.Resolve<INewsPageSteps>();
            LoginPageSteps = Container.Resolve<ILoginPageSteps>();
            NavigationBarSteps = Container.Resolve<INavigationBarSteps>();
        }

        [TearDown]
        public override void TestCleanup()
        {
            OpenBrowserSteps.CloseBrowser();
        }

        [Test]
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

        [Test]
        public void GaleryPageShown()
        {
            OpenBrowserSteps.OpenBrowser();
            OpenBrowserSteps.Navigate(TestsConstants.WebSiteHttpAddress);
            OpenBrowserSteps.CheckThatMainPageOpnned();

            NavigationBarSteps.ClickOnGalleryHeader();
            NavigationBarSteps.CheckThatGalleryPageOpnned();
        }

        [Test]
        public void TimeTablePageShown()
        {
            OpenBrowserSteps.OpenBrowser();
            OpenBrowserSteps.Navigate(TestsConstants.WebSiteHttpAddress);
            OpenBrowserSteps.CheckThatMainPageOpnned();

            NavigationBarSteps.ClickOnTimeTableHeader();
            NavigationBarSteps.CheckThatTimeTablePageOpnned();
        }

        [Test]
        public void ContactsPageShown()
        {
            OpenBrowserSteps.OpenBrowser();
            OpenBrowserSteps.Navigate(TestsConstants.WebSiteHttpAddress);
            OpenBrowserSteps.CheckThatMainPageOpnned();

            NavigationBarSteps.ClickOnContactsHeader();
            NavigationBarSteps.CheckThatContactsageOpnned();
        }

        [Test]
        public void LoginPageShown()
        {
            OpenBrowserSteps.OpenBrowser();
            OpenBrowserSteps.Navigate(TestsConstants.WebSiteHttpAddress);
            OpenBrowserSteps.CheckThatMainPageOpnned();

            NavigationBarSteps.ClickOnLoginHeader();
            NavigationBarSteps.CheckThatLoginOpnned();
        }

        [Test]
        public void NewsPageActive()
        {
            OpenBrowserSteps.OpenBrowser();
            OpenBrowserSteps.Navigate(TestsConstants.WebSiteHttpAddress);
            OpenBrowserSteps.CheckThatMainPageOpnned();
            NavigationBarSteps.CheckThatNewsPageActive();
        }

        [Test]
        public void GaleryPageActive()
        {
            OpenBrowserSteps.OpenBrowser();
            OpenBrowserSteps.Navigate(TestsConstants.WebSiteHttpAddress);
            OpenBrowserSteps.CheckThatMainPageOpnned();

            NavigationBarSteps.ClickOnGalleryHeader();
            NavigationBarSteps.CheckThatGalleryPageActive();
        }

        [Test]
        public void TimeTablePageActive()
        {
            OpenBrowserSteps.OpenBrowser();
            OpenBrowserSteps.Navigate(TestsConstants.WebSiteHttpAddress);
            OpenBrowserSteps.CheckThatMainPageOpnned();

            NavigationBarSteps.ClickOnTimeTableHeader();
            NavigationBarSteps.CheckThatTimeTablePageActive();
        }

        [Test]
        public void ContactsPageActive()
        {
            OpenBrowserSteps.OpenBrowser();
            OpenBrowserSteps.Navigate(TestsConstants.WebSiteHttpAddress);
            OpenBrowserSteps.CheckThatMainPageOpnned();

            NavigationBarSteps.ClickOnContactsHeader();
            NavigationBarSteps.CheckThatContactsButtonActive();
        }

        [Test]
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
