using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;

namespace VyatkinSchool.Tests.Steps
{
    [Binding]
    public sealed class NavigationBarSteps : INavigationBarSteps
    {
        private readonly IHeaderChecker _headerChecker;
        private readonly ILinkCliker _linkCliker;
        private readonly IButtonCliker _buttonCliker;
        private readonly IHeaderButtonChecker _headerButtonChecker;
        private readonly IBrowserNavigator _browser;

        public NavigationBarSteps(
            ILinkCliker linkCliker,
            IHeaderChecker headerChecker,
            IButtonCliker buttonCliker,
            IHeaderButtonChecker headerButtonChecker,
            IBrowserNavigator browser)
        {
            _linkCliker = linkCliker;
            _headerChecker = headerChecker;
            _buttonCliker = buttonCliker;
            _headerButtonChecker = headerButtonChecker;
            _browser = browser;
        }

        [Then(@"Check that Contacts button is active")]
        public void CheckThatContactsButtonActive()
        {
            const string buttonText = "Контакты";
            _headerButtonChecker.CheckThatHeaderButtonWithTextActive(buttonText);

            foreach(var notExpectedButton in GetNotExpectedButtons(buttonText))
            {
                _headerButtonChecker.CheckThatHeaderButtonWithTextNotActive(notExpectedButton);
            }
        }

        [Then(@"Check that Contacts page is opened")]
        public void CheckThatContactsageOpnned()
        {
            const string pageHederValue = "Контакты.";
            _headerChecker.CheckThatHeader2ContainsText(pageHederValue);
        }

        [Then(@"Check that Gallery button is active")]
        public void CheckThatGalleryPageActive()
        {
            const string buttonText = "Галерея";
            _headerButtonChecker.CheckThatHeaderButtonWithTextActive(buttonText);

            foreach (var notExpectedButton in GetNotExpectedButtons(buttonText))
            {
                _headerButtonChecker.CheckThatHeaderButtonWithTextNotActive(notExpectedButton);
            }
        }

        [Then(@"Check that Gallery page is opened")]
        public void CheckThatGalleryPageOpnned()
        {
            const string pageHederValue = "Галерея.";
            _headerChecker.CheckThatHeader2ContainsText(pageHederValue);
        }

        [Then(@"Check that Login button is active")]
        public void CheckThatLoginActive()
        {
            const string buttonText = "Выполнить вход";
            _headerButtonChecker.CheckThatHeaderButtonWithTextActive(buttonText);

            foreach (var notExpectedButton in GetNotExpectedButtons(buttonText))
            {
                _headerButtonChecker.CheckThatHeaderButtonWithTextNotActive(notExpectedButton);
            }
        }

        [Then(@"Check that Login page is opened")]
        public void CheckThatLoginOpnned()
        {
            const string pageHederValue = "Выполнить вход.";
            _headerChecker.CheckThatHeader2ContainsText(pageHederValue);
        }

        [Then(@"Check that News button is active")]
        public void CheckThatNewsPageActive()
        {
            const string buttonText = "Новости";
            _headerButtonChecker.CheckThatHeaderButtonWithTextActive(buttonText);

            foreach (var notExpectedButton in GetNotExpectedButtons(buttonText))
            {
                _headerButtonChecker.CheckThatHeaderButtonWithTextNotActive(notExpectedButton);
            }
        }

        [Then(@"Check that News page is opened")]
        public void CheckThatNewsPageOpnned()
        {
            const string pageHederValue = "Добро пожаловать на сайт Школы Вольной Борьбы";
            _headerChecker.CheckThatHeader3ContainsText(pageHederValue);
        }

        [Then(@"Check that  Time table page is active")]
        public void CheckThatTimeTablePageActive()
        {
            const string buttonText = "Расписание";
            _headerButtonChecker.CheckThatHeaderButtonWithTextActive(buttonText);

            foreach (var notExpectedButton in GetNotExpectedButtons(buttonText))
            {
                _headerButtonChecker.CheckThatHeaderButtonWithTextNotActive(notExpectedButton);
            }
        }

        [Then(@"Check that Time table page is opened")]
        public void CheckThatTimeTablePageOpnned()
        {
            const string pageHederValue = "Расписание";
            _headerChecker.CheckThatHeader2ContainsText(pageHederValue);
        }

        [When(@"Click on Contacts button in web page navigation header")]
        public void ClickOnContactsHeader()
        {
            const string linkText = "Контакты";
            _linkCliker.ClickOnLinkWithText(linkText);
        }


        [When(@"Click on Gallery button in web page navigation header")]
        public void ClickOnGalleryHeader()
        {
            const string linkText = "Галерея";
            _linkCliker.ClickOnLinkWithText(linkText);
        }

        [When(@"Click on Login button in web page navigation header")]
        public void ClickOnLoginHeader()
        {
            const string linkText = "Выполнить вход";
            _linkCliker.ClickOnLinkWithText(linkText);
        }

        [When(@"Click on News button in web page navigation header")]
        public void ClickOnNewsHeader()
        {
            const string linkText = "Новости";
            _linkCliker.ClickOnLinkWithText(linkText);
        }

        [When(@"Click on TimeTable button in web page navigation header")]
        public void ClickOnTimeTableHeader()
        {
            const string linkText = "Расписание";
            _linkCliker.ClickOnLinkWithText(linkText);
        }

        private IEnumerable<string> GetNotExpectedButtons(string expectedButton)
        {
            var notExpectedButtonsList = new List<string>
            {
                "Новости",
                "Галерея",
                 "Расписание",
                "Контакты",
                "Выполнить вход"
             };
            return notExpectedButtonsList.Except(new[] { expectedButton });
        }
    }
}
