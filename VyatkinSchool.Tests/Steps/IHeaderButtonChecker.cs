namespace VyatkinSchool.Tests.Steps
{
    public interface IHeaderButtonChecker
    {
        void CheckThatHeaderButtonWithTextActive(string linkText);
        void CheckThatHeaderButtonWithTextNotActive(string linkText);
    }
}
