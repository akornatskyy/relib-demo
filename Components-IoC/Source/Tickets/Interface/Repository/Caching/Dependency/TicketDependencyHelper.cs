using System;
using ReusableLibrary.Abstractions.Caching;
using ReusableLibrary.Supplemental.Collections;
using Tickets.Interface.Repository.Caching.Keys;

namespace Tickets.Interface.Repository.Caching.Dependency
{
    public static class TicketDependencyHelper
    {
        public static void Ticket(ICache cache, int ticketId)
        {
            //// cache.Remove(TicketKeyHelper.Retrieve(ticketId));
            var keys = new[] 
            {
                TicketKeyHelper.Retrieve(ticketId)
            };
            keys.ForEach(key => cache.Remove(key));

            new LinkedCacheDependency(cache, TicketKeyHelper.TicketDependency(), DateTime.MinValue).Remove();
        }
    }
}
