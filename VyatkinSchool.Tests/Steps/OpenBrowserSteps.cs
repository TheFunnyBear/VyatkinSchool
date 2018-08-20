using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace VyatkinSchool.Tests.Steps
{
    public sealed class OpenBrowserSteps : IOpenBrowserSteps
    {
        private readonly IBrowserNavigator _browserNavigator;
        private readonly IHeaderChecker _headerChecker;

        public OpenBrowserSteps(
            IBrowserNavigator browserNavigator,
            IHeaderChecker headerChecker)
        {
            _browserNavigator = browserNavigator;
            _headerChecker = headerChecker;
        }

        [Given(@"Open Firefox browser")]
        public void OpenBrowser()
        {
            _browserNavigator.Open();
        }

        [When(@"Navigate in Firefox browser to (.*)")]
        public void Navigate(string webSiteAddress)
        {
            _browserNavigator.Navigate(webSiteAddress);
        }

        [Then(@"Navigate in Firefox browser to (.*)")]
        public void TitleDisplayed(string expectedTitleText)
        {
            Assert.AreEqual(expectedTitleText, _browserNavigator.GetTitle());
        }

        [AfterScenario(@"Close browser")]
        public void CloseBrowser()
        {
            _browserNavigator.Close();
        }

        [Then(@"Check than main page opened")]
        public void CheckThatMainPageOpnned()
        {
            const string creatMessagePageText = "ДОБРО ПОЖАЛОВАТЬ НА САЙТ ШКОЛЫ ВОЛЬНОЙ БОРЬБЫ - ОЛЕГА ВЯТКИНА";
            _headerChecker.CheckThatHeaderContainsText(creatMessagePageText);
        }
    }
}
