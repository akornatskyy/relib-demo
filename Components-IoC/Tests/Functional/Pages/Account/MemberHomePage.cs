using Public.FunctionalTests.Pages.Home;
using WatiN.Core;
using Xunit;

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
