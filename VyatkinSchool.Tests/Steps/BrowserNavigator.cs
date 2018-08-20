using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;

namespace VyatkinSchool.Tests.Steps
{
    public sealed class BrowserNavigator : IBrowserNavigator
    {
        public FirefoxDriver Browser { get; set; }

        public void Open()
        {
            Browser = new FirefoxDriver();
        }

        public void Close()
        {
            Browser.Close();
        }

        public void Navigate(string webSiteAddress)
        {
            Browser.Navigate().GoToUrl(webSiteAddress);
        }

        public string GetTitle()
        {
            return Browser.Title;
        }

        public RemoteWebDriver GetDriver()
        {
            return Browser as RemoteWebDriver;
        }
    }
}
