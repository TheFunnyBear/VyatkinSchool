using OpenQA.Selenium;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace VyatkinSchool.Tests.Steps
{
    public sealed class LoginPageSteps : ILoginPageSteps
    {
        private readonly ILinkCliker _linkCliker;
        private readonly IButtonCliker _buttonCliker;
        private readonly IBrowserNavigator _browser;

        public LoginPageSteps(
            ILinkCliker linkCliker,
            IButtonCliker buttonCliker,
            IBrowserNavigator browser)
        {
            _linkCliker = linkCliker;
            _buttonCliker = buttonCliker;
            _browser = browser;
        }

        [When(@"Click on Login button")]
        public void ClickOnLoginButton()
        {
            const string butonText = "Войти";
            _buttonCliker.ClickOnButtonWithText(butonText);
        }

        [When(@"Click on Login button")]
        public void ClickOnLoginMenuItem()
        {
            const string linkText = "Вход";
            _linkCliker.ClickOnLinkWithText(linkText);
        }

        [When(@"Login as admin")]
        public void Login()
        {
            const string password = "123";
            ClickOnLoginMenuItem();
            SetPasswordText(password);
            ClickOnLoginButton();
        }

        [When(@"Set password (.*) to  password edit-box")]
        public void SetPasswordText(string password)
        {
            var passwordElement = _browser.GetDriver().FindElement(By.Id("Password"));
            Assert.IsNotNull(passwordElement, "Can't find password at the page.");
            passwordElement.Click();
            passwordElement.Clear();
            passwordElement.SendKeys(password);
        }
    }
}