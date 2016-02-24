using Xunit;

namespace Public.FunctionalTests.Pages.Home
{
    public sealed class AboutPage : SitePage
    {
        public static readonly string Uri = string.Empty;

        protected override void InitializeContents()
        {
            Container = Document.Div("about");
            Assert.True(Document.Title.Contains("About"));

            base.InitializeContents();
        }
    }
}