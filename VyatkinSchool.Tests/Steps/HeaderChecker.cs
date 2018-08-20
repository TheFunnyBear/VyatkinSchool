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

        public void CheckThatHeaderContainsText(string text)
        {
            var headers = _browser.GetDriver().FindElements(By.TagName("h2"));
            var creatMessageHeader = headers.SingleOrDefault(element => element.Text.Equals(text, StringComparison.OrdinalIgnoreCase));

            if (creatMessageHeader == null)
            {
                var existedLinks = string.Join(", ", headers.Select(tag => $"[Tag: {tag.TagName}, Text: {tag.Text}]"));
                Assert.Fail($"Can't find [{text}] link in page. Existed links is: [{existedLinks}]");
            }
        }
    }
}
