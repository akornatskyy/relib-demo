using Tickets.Interface.Models;
using Xunit;

namespace Public.WebMvcTests
{
    public static class AssertHelper
    {
        public static void Equal(TicketSpecification expected, TicketSpecification actual)
        {
            Assert.Equal(expected.Title, actual.Title);
            Assert.Equal(expected.TicketTypeId, actual.TicketTypeId);
            Assert.Equal(expected.IncludeClosed, actual.IncludeClosed);
        }
    }
}
