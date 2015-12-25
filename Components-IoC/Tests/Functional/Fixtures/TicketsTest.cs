using System;
using Public.FunctionalTests.Pages.Account;
using Public.FunctionalTests.Services;
using ReusableLibrary.Supplemental.Collections;
using ReusableLibrary.Supplemental.System;
using Tickets.Interface.Models;
using Xunit;
using Public.FunctionalTests.Pages.Tickets;

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
        [Trait(Constants.TraitNames.Tickets, "UC2.2.1. Default search")]
        public void Search_By_Default()
        {
            var errors = Service.Search(new TicketSpecification());
            Assert.Empty(errors);
            Browser.Page<TicketsListPage>();
        }

        [Fact]
        [Trait(Constants.TraitNames.Tickets, "UC2.2.2. Search by title")]
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
