using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System.Collections.Generic;
using System.Linq;

namespace VyatkinSchool.Tests.Steps
{
    public sealed class LinkCliker : ILinkCliker
    {
        private readonly IBrowserNavigator _browser;

        public LinkCliker(IBrowserNavigator browser)
        {
            _browser = browser;
        }

        public void ClickOnLinkWithText(string linkText)
        {
            var linkWithText = FindIsLinkWithText(linkText);
            Assert.IsNotNull(linkWithText,  $"Can't find [{linkText}] link in page. Existed links is: [{GetExistedLinks()}]");
            
            linkWithText.Click();
        }

        public void CheckThatLinkWithTextExist(string linkText)
        {
            var linkWithText = FindIsLinkWithText(linkText);
            Assert.IsNotNull(linkWithText, $"Can't find [{linkText}] link in page. Existed links is: [{GetExistedLinks()}]");
        }

        public void CheckThatLinkWithTextNotExist(string linkText)
        {
            var linkWithText = FindIsLinkWithText(linkText);
            Assert.IsNull(linkWithText, $"Found [{linkText}] link in page. But should not.");
        }

        private IWebElement FindIsLinkWithText(string linkText)
        {
            IList<IWebElement> links = _browser.GetDriver().FindElements(By.TagName("a"));
            return links.SingleOrDefault(element => element.Text.Equals(linkText, StringComparison.OrdinalIgnoreCase));
        }

        private string GetExistedLinks()
        {
            var existedLinks = _browser.GetDriver().FindElements(By.TagName("a"));
            return string.Join(", ", existedLinks.Select(link => $"[Tag: {link.TagName}, Text: {link.Text}]"));
        }

        

    }
}
