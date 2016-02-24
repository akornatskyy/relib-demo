using System;
using System.Collections.Specialized;

using ReusableLibrary.WatiN;

using Public.FunctionalTests.Pages.Tickets;
using Tickets.Interface.Models;

namespace Public.FunctionalTests.Services
{
    public sealed class TicketsService : AbstractService
    {
        public NameValueCollection Search(TicketSpecification specification)
        {
            var page = Browser.Page<TicketsSearchPage>();
            return page.Container
                .Fill(specification)
                .Submit()
                .OnErrorMessage(m => { throw new InvalidOperationException(m); })
                .ValidationErrors();
        }
    }
}