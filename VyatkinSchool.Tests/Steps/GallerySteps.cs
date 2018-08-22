using System;
using TechTalk.SpecFlow;
using VyatkinSchool.Tests.Constants;

namespace VyatkinSchool.Tests.Steps
{
    [Binding]
    public class GallerySteps
    {
        const string AddImageButtonText = "Добавить изображение";

        private readonly IHeaderChecker _headerChecker;
        private readonly ILinkCliker _linkCliker;
        private readonly IButtonCliker _buttonCliker;
        private readonly ITestsConstants _testsConstants;
        private readonly IOpenBrowserSteps _openBrowserSteps;
        private readonly ILoginPageSteps _loginPageSteps;
        private readonly INavigationBarSteps _navigationBarSteps;
        private readonly IBrowserNavigator _browser;

        public GallerySteps(
            ILinkCliker linkCliker,
            IHeaderChecker headerChecker,
            IButtonCliker buttonCliker,
            ITestsConstants testsConstants,
            ILoginPageSteps loginPageSteps,
            INavigationBarSteps navigationBarSteps,
            IOpenBrowserSteps openBrowserSteps,
            IBrowserNavigator browser)
        {
            _linkCliker = linkCliker;
            _headerChecker = headerChecker;
            _buttonCliker = buttonCliker;
            _testsConstants = testsConstants;
            _loginPageSteps = loginPageSteps;
            _navigationBarSteps = navigationBarSteps;
            _openBrowserSteps = openBrowserSteps;
            _browser = browser;
        }

        [Given(@"Navigate to main page")]
        public void GivenNavigateToMainPage()
        {
            _browser.Open();
            _browser.Navigate(_testsConstants.WebSiteHttpAddress);
        }

        [Given(@"Main page is shown")]
        public void GivenMainPageIsShown()
        {
            _openBrowserSteps.CheckThatMainPageOpnned();
        }

        [Given(@"Login as administrator")]
        public void GivenLoginAsAdministrator()
        {
            _loginPageSteps.Login();
        }
        
        [Given(@"Click on News button in navigation bar")]
        public void GivenClickOnNewsButtonInNavigationBar()
        {
            _navigationBarSteps.ClickOnNewsHeader();
        }
        
        [When(@"Click on Gallery button in navigation bar")]
        public void WhenClickOnGalleryButtonInNavigationBar()
        {
            _navigationBarSteps.ClickOnGalleryHeader();
        }
        
        [Then(@"can't add image to gallery")]
        public void ThenTheCanTAddImageToGallery()
        {
            _linkCliker.CheckThatLinkWithTextNotExist(AddImageButtonText);
        }
        
        [Then(@"can add image to gallery")]
        public void ThenTheCanAddImageToGallery()
        {
            _linkCliker.CheckThatLinkWithTextExist(AddImageButtonText);
        }

        [AfterScenario("galleryFunctionalTest")]
        public void AfterScenario()
        {
            _browser.Close();
        }
    }
}
