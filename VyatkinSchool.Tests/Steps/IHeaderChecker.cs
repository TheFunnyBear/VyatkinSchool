namespace VyatkinSchool.Tests.Steps
{
    public interface IHeaderChecker
    {
        void CheckThatHeader1ContainsText(string text);
        void CheckThatHeader2ContainsText(string text);
        void CheckThatHeader3ContainsText(string text);
        void CheckThatHeaderContainsText(string hederNumber, string text);
    }
}
