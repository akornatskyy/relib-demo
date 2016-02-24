using Xunit;

using Public.FunctionalTests.Pages.Home;

namespace Public.FunctionalTests.Pages.Account
{
    public sealed class MemberHomePage : HomePage
    {
        protected override void InitializeContents()
        {
            Assert.True(Document.Div("logindisplay").Text.Contains("Welcome"));

            base.InitializeContents();
        }
    }
}