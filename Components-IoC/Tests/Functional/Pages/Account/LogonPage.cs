using Xunit;

namespace Public.FunctionalTests.Pages.Account
{
    public sealed class LogonPage : SitePage
    {
        public static readonly string Uri = "/account/logon";

        protected override void InitializeContents()
        {
            Container = Document.Div("logon");
            Assert.True(Document.Title.Contains("Log On"));

            base.InitializeContents();
        }
    }
}