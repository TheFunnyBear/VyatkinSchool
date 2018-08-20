using OpenQA.Selenium.Firefox;

namespace VyatkinSchool.Tests.Steps
{
    public interface IOpenBrowserSteps
    {
        void Navigate(string webSiteAddress);
        void OpenBrowser();
        void TitleDisplayed(string expectedTitleText);
        void CloseBrowser();
        void CheckThatMainPageOpnned();
    }
}