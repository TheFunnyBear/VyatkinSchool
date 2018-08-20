using OpenQA.Selenium.Firefox;

namespace VyatkinSchool.Tests.Steps
{
    public interface ILoginPageSteps
    {
        void Login();
        void ClickOnLoginMenuItem();
        void SetPasswordText(string password);
        void ClickOnLoginButton();
    }
}
