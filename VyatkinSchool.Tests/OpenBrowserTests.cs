using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;
using Unity.Attributes;
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
            TestsConstants = Container.Resolve<ITestsConstants>();
            OpenBrowserSteps = Container.Resolve<IOpenBrowserSteps>();
        }

        [TestCleanup]
        public override void TestCleanup()
        {
            OpenBrowserSteps.CloseBrowser();
        }

        [TestMethod]
        public void OpenBrowser()
        {
            OpenBrowserSteps.OpenBrowser();
            OpenBrowserSteps.Navigate(TestsConstants.WebSiteHttpAddress);
            OpenBrowserSteps.TitleDisplayed(TestsConstants.WebSiteTitle);
        }
    }
}
