using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace VyatkinSchool.Tests.Steps
{
    public sealed class HeaderButtonChecker: IHeaderButtonChecker
    {
        private readonly IBrowserNavigator _browser;

        public HeaderButtonChecker(IBrowserNavigator browser)
        {
            _browser = browser;
        }

        public void CheckThatHeaderButtonWithTextActive(string linkText)
        {
            var listItemWithExpectedText = FindHeaderButtonWithText(linkText);
            Assert.IsTrue(listItemWithExpectedText.GetAttribute("class").Equals("active"), $"Header button with text [{linkText}] not active");
        }

        public void CheckThatHeaderButtonWithTextNotActive(string linkText)
        {
            var listItemWithExpectedText = FindHeaderButtonWithText(linkText);
            Assert.IsFalse(listItemWithExpectedText.GetAttribute("class").Equals("active"), $"Header button with text [{linkText}] active, but should not");
        }

        private IWebElement FindHeaderButtonWithText(string linkText)
        {
            var menu =_browser.GetDriver().FindElements(By.Id("menu")).SingleOrDefault();
            if (menu == null)
            {
                Assert.Fail("Can't find menu element with links");
            }

            IList<IWebElement> listItemElements = menu.FindElements(By.TagName("li"));

            var listItemWithExpectedText = listItemElements.SingleOrDefault(element => {
                var embededLinks = element.FindElements(By.TagName("a"));
                var embededLink = embededLinks.SingleOrDefault();
                if (embededLink == null)
                {
                    Assert.Fail("Can't find list item with embed link");
                }
                else
                {
                    return embededLink.Text.Equals(linkText, StringComparison.OrdinalIgnoreCase);
                }
                return false;
            }
            );

            if (listItemWithExpectedText == null)
            {
                Assert.Fail($"Can't find header button with text [{linkText}] in page.");
            }

            return listItemWithExpectedText;
        }
    }
}
