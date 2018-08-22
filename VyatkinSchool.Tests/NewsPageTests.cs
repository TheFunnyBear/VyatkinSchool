using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.Unity;
using VyatkinSchool.Tests.Steps;
using VyatkinSchool.Tests.Constants;
using System;

namespace VyatkinSchool.Tests
{
    [TestClass]
    public sealed class NewsPageTests : UnitTestBase
    {
        [Dependency]
        public ITestsConstants TestsConstants { get; set; }

        [Dependency]
        public IOpenBrowserSteps OpenBrowserSteps { get; set; }

        [Dependency]
        public INewsPageSteps NewsPageSteps { get; set; }

        [Dependency]
        public ILoginPageSteps LoginPageSteps { get; set; }

        [Dependency]
        public IMessageCreatedSteps MessageCreatedSteps { get; set; }

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            TestsConstants = Container.Resolve<ITestsConstants>();
            OpenBrowserSteps = Container.Resolve<IOpenBrowserSteps>();
            NewsPageSteps = Container.Resolve<INewsPageSteps>();
            LoginPageSteps = Container.Resolve<ILoginPageSteps>();
            MessageCreatedSteps = Container.Resolve<IMessageCreatedSteps>();
        }

        [TestCleanup]
        public override void TestCleanup()
        {
            OpenBrowserSteps.CloseBrowser();
        }

        [TestMethod]
        public void NewMessagePageShown()
        {
            OpenBrowserSteps.OpenBrowser();
            OpenBrowserSteps.Navigate(TestsConstants.WebSiteHttpAddress);
            OpenBrowserSteps.CheckThatMainPageOpnned();
            LoginPageSteps.Login();
            NewsPageSteps.ClickOnAddNewMessageButton();
            NewsPageSteps.CheckThatNewMessagePageOpened();
        }

        [TestMethod]
        public void NewMessagePageTitle()
        {
            OpenBrowserSteps.OpenBrowser();
            OpenBrowserSteps.Navigate(TestsConstants.WebSiteHttpAddress);
            OpenBrowserSteps.CheckThatMainPageOpnned();
            LoginPageSteps.Login();
            NewsPageSteps.ClickOnAddNewMessageButton();

            const string pageTitle = "Школа борьбы: Создать сообщение";
            OpenBrowserSteps.TitleDisplayed(pageTitle);
        }

        [TestMethod]
        public void AddMessageWithPicture()
        {
            OpenBrowserSteps.OpenBrowser();
            OpenBrowserSteps.Navigate(TestsConstants.WebSiteHttpAddress);
            OpenBrowserSteps.CheckThatMainPageOpnned();
            LoginPageSteps.Login();
            NewsPageSteps.ClickOnAddNewMessageButton();

            var messageGuid = Guid.NewGuid().ToString();
            var title = $"Это тестовое сообщение c картинкой {messageGuid}";
            var message = $"Это текст тестового сообщения c картинкой, This is text for test message with picture {messageGuid}";
            NewsPageSteps.AddTitle(title);
            NewsPageSteps.AddMessageText(message);
            NewsPageSteps.AddPicture();
            NewsPageSteps.ClickOnCreateMessageButon();

            MessageCreatedSteps.CheckThatNewMessageCreatedPageOpened();
            MessageCreatedSteps.CheckThatTitleShownForNewMessage(title);
            MessageCreatedSteps.CheckThatTextShownForNewMessage(message);
            const string pageTitle = "Школа борьбы: Добавление сообщения";
            OpenBrowserSteps.TitleDisplayed(pageTitle);

            MessageCreatedSteps.ClickOnShowAllMessagesList();
            OpenBrowserSteps.CheckThatMainPageOpnned();
            NewsPageSteps.CheckThatMainPageContainsMessageWithPicture(title);
        }

        [TestMethod]
        public void AddMessageWithowtPicture()
        {
            OpenBrowserSteps.OpenBrowser();
            OpenBrowserSteps.Navigate(TestsConstants.WebSiteHttpAddress);
            OpenBrowserSteps.CheckThatMainPageOpnned();
            LoginPageSteps.Login();
            NewsPageSteps.ClickOnAddNewMessageButton();

            var messageGuid = Guid.NewGuid().ToString();
            var title = $"Это тестовое сообщение {messageGuid}";
            var message = $"Это текст тестового сообщения, This is text for test message {messageGuid}";
            NewsPageSteps.AddTitle(title);
            NewsPageSteps.AddMessageText(message);
            NewsPageSteps.ClickOnCreateMessageButon();

            MessageCreatedSteps.CheckThatNewMessageCreatedPageOpened();
            MessageCreatedSteps.CheckThatTitleShownForNewMessage(title);
            MessageCreatedSteps.CheckThatTextShownForNewMessage(message);
            const string pageTitle = "Школа борьбы: Добавление сообщения";
            OpenBrowserSteps.TitleDisplayed(pageTitle);

            MessageCreatedSteps.ClickOnShowAllMessagesList();
            OpenBrowserSteps.CheckThatMainPageOpnned();
            NewsPageSteps.CheckThatMainPageContainsMessage(title);
        }
    }
}
