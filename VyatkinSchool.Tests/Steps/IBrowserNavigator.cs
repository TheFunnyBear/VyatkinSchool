using OpenQA.Selenium.Remote;

namespace VyatkinSchool.Tests.Steps
{
    public interface IBrowserNavigator
    {
        void Open();
        void Close();
        void Navigate(string webSiteAddress);
        string GetTitle();
        RemoteWebDriver GetDriver();
    }
}
