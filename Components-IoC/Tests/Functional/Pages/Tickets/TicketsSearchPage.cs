using Xunit;

namespace Public.FunctionalTests.Pages.Tickets
{
    public sealed class TicketsSearchPage : SitePage
    {
        public static readonly string Uri = "/en/tickets";

        protected override void InitializeContents()
        {
            Container = Document.Div("ticketssearch");
            Assert.True(Document.Title.Contains("Search Tickets"));

            base.InitializeContents();
        }
    }
}