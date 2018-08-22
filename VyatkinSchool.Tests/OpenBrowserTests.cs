using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.Unity;
using VyatkinSchool.Tests.Steps;
using VyatkinSchool.Tests.Constants;

namespace VyatkinSchool.Tests
{
    [TestClass]
    public sealed class OpenBrowserTests : UnitTestBase
    {
        [Dependency]
        public ITestsConstants TestsConstants { get; set; }

        [Dependency]
        public IOpenBrowserSteps OpenBrowserSteps { get; set; }

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            TestsConstants = Container.Resolve<ITestsConstants>();
            OpenBrowserSteps = Container.Resolve<IOpenBrowserSteps>();
        }

        [TestCleanup]
        public override void TestCleanup()
        {
            OpenBrowserSteps.CloseBrowser();
        }

        [TestMethod]
        public void TitleDisplayed()
        {
            OpenBrowserSteps.OpenBrowser();
            OpenBrowserSteps.Navigate(TestsConstants.WebSiteHttpAddress);
            OpenBrowserSteps.TitleDisplayed(TestsConstants.WebSiteTitle);
        }
    }
}
