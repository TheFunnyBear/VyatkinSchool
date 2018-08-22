namespace VyatkinSchool.Tests.Steps
{

    public interface ILinkCliker
    {
        void CheckThatLinkWithTextExist(string linkText);
        void CheckThatLinkWithTextNotExist(string linkText);
        void ClickOnLinkWithText(string linkText);
    }
}
