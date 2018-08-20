using OpenQA.Selenium.Firefox;

namespace VyatkinSchool.Tests.Steps
{
    public interface INewsPageSteps
    {
        void ClickOnAddNewMessageButton();
        void CheckThatNewMessagePageOpened();
        void AddTitle(string title);
        void AddMessageText(string message);
        void ClickOnCreateMessageButon();
    }
}
