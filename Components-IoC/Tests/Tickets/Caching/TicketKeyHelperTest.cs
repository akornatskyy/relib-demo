using ReusableLibrary.Abstractions.Repository;
using Xunit;
using Xunit.Extensions;

using Tickets.Interface.Models;
using Tickets.Interface.Repository;
using Tickets.Interface.Repository.Caching;
using Tickets.Interface.Repository.Caching.Keys;
using Tickets.Tests.Constants;
using Tickets.Tests.Infrastructure;

namespace Tickets.Tests.Caching
{
    public sealed class TicketKeyHelperTest : AbstractKeyHelperTest<ITicketRepository, TicketRepository>
    {
        [Theory]
        [InlineData(105)]
        [InlineData(501)]
        [Trait(TraitNames.Caching, "Ticket")]
        public void RetrieveKey(int id)
        {
            var key = TicketKeyHelper.Retrieve(id);
            Run(cacheMock => SetupRetrieve<Ticket>(cacheMock, key).Retrieve(id),
                cacheMock => SetupRetrieveAndStore<Ticket>(cacheMock, key).Retrieve(id),
                cacheMock => SetupRemove(cacheMock, key).UpdateTicket("x", new Ticket() { TicketId = id }));
        }

        [Fact]
        [Trait(TraitNames.Caching, "Ticket")]
        public void TicketDependency()
        {
            var key = TicketKeyHelper.TicketDependency();
            Run(cacheMock => SetupDependencyRemove(cacheMock, key).UpdateTicket("x", new Ticket()),
                cacheMock => SetupDependencyAdd(cacheMock, key).RetrieveMultiple(new RetrieveMultipleRequest<TicketSpecification>(new TicketSpecification())));
        }
    }
}