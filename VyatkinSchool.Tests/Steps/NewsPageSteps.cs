using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System.IO;
using System.Linq;
using TechTalk.SpecFlow;
using Resources;
using System.Drawing;

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

        [Then(@"Check that main page with news contains (.*) message")]
        public void CheckThatMainPageContainsMessage(string title)
        {
            var boxElements = _browser.GetDriver().FindElements(By.ClassName("box"));
            var isMessageWithTitleExist = boxElements.Any(element => {
                var boxElementsWithDetails = element.FindElements(By.ClassName("details"));
                if (boxElementsWithDetails != null)
                {
                    return boxElementsWithDetails.Any(detailsElement =>
                    {
                        var paragraphTags = detailsElement.FindElements(By.TagName("p"));
                        if (paragraphTags != null)
                        {
                            return paragraphTags.Any(paragraphTag => paragraphTag.Text.Equals(title));
                        }

                        return false;
                    });
                }

                return false;

            });

            Assert.IsTrue(isMessageWithTitleExist, $"Can't find [{title}] in main page.");
        }

        [Then(@"Add picture to new message")]
        public void AddPicture()
        {
            var converter = new ImageConverter();
            var pictureBytes = (byte[])converter.ConvertTo(Resources.VyatkinSchool.TestPicture, typeof(byte[]));

            var saveTempPictureTo = Path.Combine(Path.GetTempPath(), $"{Path.GetFileNameWithoutExtension(Path.GetTempFileName())}.jpg");
            File.WriteAllBytes(saveTempPictureTo, pictureBytes);

            var element = _browser.GetDriver().FindElement(By.Id("uploadImageFile"));
            element.SendKeys(saveTempPictureTo);
        }

        public void CheckThatMainPageContainsMessageWithPicture(string title)
        {
            var boxElements = _browser.GetDriver().FindElements(By.ClassName("box"));
            var isPictureExist = false;

            var boxElementsWithDetails = boxElements.Where(element => element.FindElements(By.ClassName("details")).Any());
            var boxElementWithExpectedTitle = boxElementsWithDetails.SingleOrDefault(detailsElement =>
            {
                var paragraphTags = detailsElement.FindElements(By.TagName("p"));
                if (paragraphTags != null)
                {
                    return paragraphTags.Any(paragraphTag => paragraphTag.Text.Equals(title));
                }

                return false;
            });

            Assert.IsNotNull(boxElementWithExpectedTitle, $"Can't find [{title}] in main page.");


            var imagElement = boxElementWithExpectedTitle.FindElements(By.TagName("img")).SingleOrDefault();
            Assert.IsNotNull(imagElement, $"Can't find picture in message with title[{title}] in main page.");
        }
    }
}
