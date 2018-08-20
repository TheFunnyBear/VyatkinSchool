using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System.Collections.Generic;
using System.Linq;

namespace VyatkinSchool.Tests.Steps
{
    public sealed class ButtonCliker : IButtonCliker
    {
        private readonly IBrowserNavigator _browser;

        public ButtonCliker(IBrowserNavigator browser)
        {
            _browser = browser;
        }

        public void ClickOnButtonWithText(string linkText)
        {
            IList<IWebElement> links = _browser.GetDriver().FindElements(By.TagName("button"));
            var creatMessageLink = links.SingleOrDefault(element => element.Text.Equals(linkText, StringComparison.OrdinalIgnoreCase));
            if (creatMessageLink == null)
            {
                var existedLinks = string.Join(", ", links.Select(link => $"Tag: {link.TagName}, Text: {link.Text}"));
                Assert.Fail($"Can't find [{linkText}] link in page. Existed links is: [{existedLinks}]");
            }
            creatMessageLink.Click();
        }
    }
}
