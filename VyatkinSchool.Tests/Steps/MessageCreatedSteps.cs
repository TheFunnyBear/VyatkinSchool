using OpenQA.Selenium;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using System.Linq;

namespace VyatkinSchool.Tests.Steps
{
    public sealed class MessageCreatedSteps : IMessageCreatedSteps
    {
        private readonly IHeaderChecker _headerChecker;
        private readonly ILinkCliker _linkCliker;
        private readonly IButtonCliker _buttonCliker;
        private readonly IBrowserNavigator _browser;

        public MessageCreatedSteps(
            ILinkCliker linkCliker,
            IButtonCliker buttonCliker,
            IHeaderChecker headerChecker,
            IBrowserNavigator browser)
        {
            _linkCliker = linkCliker;
            _buttonCliker = buttonCliker;
            _headerChecker = headerChecker;
            _browser = browser;
        }

        [Then(@"Check that page opened for new message")]
        public void CheckThatNewMessageCreatedPageOpened()
        {
            const string creatMessagePageText = "Добавление сообщения";
            _headerChecker.CheckThatHeader2ContainsText(creatMessagePageText);
        }

        [Then(@"Check that title shown (.*) for new message")]
        public void CheckThatTitleShownForNewMessage(string title)
        {
            _headerChecker.CheckThatHeader2ContainsText(title);
        }

        [Then(@"Check that text shown (.*) for new message")]
        public void CheckThatTextShownForNewMessage(string text)
        {
            var paragraphs = _browser.GetDriver().FindElements(By.TagName("p"));
            var creatMessageHeader = paragraphs.SingleOrDefault(element => element.Text.Equals(text, StringComparison.OrdinalIgnoreCase));

            if (creatMessageHeader == null)
            {
                var existedLinks = string.Join(", ", paragraphs.Select(tag => $"[Tag: {tag.TagName}, Text: {tag.Text}]"));
                Assert.Fail($"Can't find [{text}] in paragraph in page. Existed paragraphs is: [{existedLinks}]");
            }
        }

        [When(@"Click on add another message button")]
        public void ClickOnAddAnotherMessageButton()
        {
            const string linkText = "Добавить ещё одно собщение";
            _linkCliker.ClickOnLinkWithText(linkText);
        }

        [When(@"Click on add another message button")]
        public void ClickOnShowAllMessagesList()
        {
            const string linkText = "Перейти к списку новостей";
            _linkCliker.ClickOnLinkWithText(linkText);
        }
    }
}