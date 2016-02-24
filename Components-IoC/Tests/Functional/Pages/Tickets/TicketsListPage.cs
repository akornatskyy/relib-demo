using Xunit;

namespace Public.FunctionalTests.Pages.Tickets
{
    public sealed class TicketsListPage : SitePage
    {
        protected override void InitializeContents()
        {
            Container = Document.Div("ticketslist");
            Assert.True(Document.Title.Contains("Tickets List"));

            base.InitializeContents();
        }
    }
}