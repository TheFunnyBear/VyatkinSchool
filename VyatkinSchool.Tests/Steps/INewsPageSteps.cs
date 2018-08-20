namespace VyatkinSchool.Tests.Steps
{
    public interface INewsPageSteps
    {
        void ClickOnAddNewMessageButton();
        void CheckThatNewMessagePageOpened();
        void AddTitle(string title);
        void AddMessageText(string message);
        void ClickOnCreateMessageButon();
        void CheckThatMainPageContainsMessage(string title);
        void AddPicture();
        void CheckThatMainPageContainsMessageWithPicture(string title);
    }
}
