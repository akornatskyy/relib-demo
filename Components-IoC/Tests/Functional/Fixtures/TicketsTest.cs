using Xunit;

using Public.FunctionalTests.Constants;
using Public.FunctionalTests.Infrastructure;
using Public.FunctionalTests.Pages.Tickets;
using Public.FunctionalTests.Services;
using Tickets.Interface.Models;

namespace Public.FunctionalTests.Fixtures
{
    public sealed class TicketsTest : DemoUserPerFixtureLifeTimeTest<TicketsService>
    {
        public override void SetFixture(DefaultLifeTimeContainer data)
        {
            base.SetFixture(data);
            Browser.GoTo(data.Application.RelativeUri(TicketsSearchPage.Uri));
        }

        [Fact]
        [Trait(TraitNames.Tickets, "UC2.2.1. Default search")]
        public void Search_By_Default()
        {
            var errors = Service.Search(new TicketSpecification());
            Assert.Empty(errors);
            Browser.Page<TicketsListPage>();
        }

        [Fact]
        [Trait(TraitNames.Tickets, "UC2.2.2. Search by title")]
        public void Search_By_Title()
        {
            var errors = Service.Search(new TicketSpecification() 
            { 
                Title = "Phone"
            });
            Assert.Empty(errors);
            Browser.Page<TicketsListPage>();
        }
    }
}
