namespace VyatkinSchool.Tests.Steps
{
    public interface IMessageCreatedSteps
    {
        void CheckThatNewMessageCreatedPageOpened();
        void CheckThatTitleShownForNewMessage(string title);
        void CheckThatTextShownForNewMessage(string text);
        void ClickOnAddAnotherMessageButton();
        void ClickOnShowAllMessagesList();
    }
}
