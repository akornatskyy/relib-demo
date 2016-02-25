using System.Linq;

using Tickets.Tests.Fixtures;
using Tickets.Tests.Infrastructure;

namespace Tickets.Tests
{
    public static class Program
    {
        public static void Main()
        {
            using (var test = new TicketRepositoryTest())
            {
                test.RetrieveMultiple(DomainModelFactory.RandomTicketSpecifications().First());
            }

            // var test = new Profiling.TicketRepositoryProfilingTest();
            // test.UpdateTicket(8);
        }
    }
}