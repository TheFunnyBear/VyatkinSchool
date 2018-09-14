using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System.Linq;

namespace VyatkinSchool.Tests.Steps
{
    public sealed class HeaderChecker : IHeaderChecker
    {
        private readonly IBrowserNavigator _browser;

        public HeaderChecker(IBrowserNavigator browser)
        {
            _browser = browser;
        }

        public void CheckThatHeader1ContainsText(string text)
        {
            CheckThatHeaderContainsText("h1", text);
        }

        public void CheckThatHeader2ContainsText(string text)
        {
            CheckThatHeaderContainsText("h2", text);
        }

        public void CheckThatHeader3ContainsText(string text)
        {
            CheckThatHeaderContainsText("h3", text);
        }

        public void CheckThatHeaderContainsText(string hederNumber, string text)
        {
            var headers = _browser.GetDriver().FindElements(By.TagName(hederNumber));
            var creatMessageHeader = headers.SingleOrDefault(element => element.Text.Equals(text, StringComparison.OrdinalIgnoreCase));

            if (creatMessageHeader == null)
            {
                var existedLinks = string.Join(", ", headers.Select(tag => $"[Tag: {tag.TagName}, Text: {tag.Text}]"));
                Assert.Fail($"Can't find [{text}] link in page. Existed links is: [{existedLinks}]");
            }
        }
    }
}
