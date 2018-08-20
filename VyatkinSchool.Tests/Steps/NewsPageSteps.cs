using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace VyatkinSchool.Tests.Steps
{
    [Binding]
    public sealed class NewsPageSteps : INewsPageSteps
    {
        private readonly IHeaderChecker _headerChecker;
        private readonly ILinkCliker _linkCliker;
        private readonly IButtonCliker _buttonCliker;
        private readonly IBrowserNavigator _browser;

        public NewsPageSteps(
            ILinkCliker linkCliker, 
            IHeaderChecker headerChecker,
            IButtonCliker buttonCliker,
            IBrowserNavigator browser)
        {
            _linkCliker = linkCliker;
            _headerChecker = headerChecker;
            _buttonCliker = buttonCliker;
            _browser = browser;
        }

        [When(@"Click on add news button")]
        public void ClickOnAddNewMessageButton()
        {
            const string creatMessageText = "Создать сообщение";
            _linkCliker.ClickOnLinkWithText(creatMessageText);
        }

        [Then(@"Check that add news page opened")]
        public void CheckThatNewMessagePageOpened()
        {
            const string creatMessagePageText = "Создать сообщение";
            _headerChecker.CheckThatHeaderContainsText(creatMessagePageText);
        }

        [When(@"Add title (.*) for new message")]
        public void AddTitle(string title)
        {
            var titleElement = _browser.GetDriver().FindElement(By.Id("Title"));
            Assert.IsNotNull(titleElement, "Can't find title edit-box at the page.");
            titleElement.Click();
            titleElement.Clear();
            titleElement.SendKeys(title);
        }

        [When(@"Add text (.*)  for new message")]
        public void AddMessageText(string message)
        {
            //Read more how to test message text https://yizeng.me/2014/01/31/test-wysiwyg-editors-using-selenium-webdriver/
            _browser.GetDriver().ExecuteScript($"tinyMCE.activeEditor.insertContent('<p>{message}</p>')");
        }

        [When(@"Click on button for create new message")]
        public void ClickOnCreateMessageButon()
        {
            const string creatMessageText = "Создать сообщение";
            _buttonCliker.ClickOnButtonWithText(creatMessageText);
        }
    }
}
