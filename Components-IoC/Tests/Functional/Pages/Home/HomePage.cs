using Xunit;

namespace Public.FunctionalTests.Pages.Home
{
    public class HomePage : SitePage
    {
        public static readonly string Uri = string.Empty;

        protected override void InitializeContents()
        {
            Container = Document.Div("welcome");
            Assert.True(Document.Title.Contains("Home"));

            base.InitializeContents();
        }
    }
}