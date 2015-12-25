using System;
using System.Collections.Generic;
using ReusableLibrary.Abstractions.Caching;
using ReusableLibrary.Abstractions.Repository;
using ReusableLibrary.Supplemental.Caching;
using Tickets.Interface.Models;
using Tickets.Interface.Repository.Caching.Dependency;

namespace Tickets.Interface.Repository.Caching
{
    public sealed class TicketRepository : Decorated.TicketRepository
    {
        private readonly ICache m_cache;

        public TicketRepository(ITicketRepository innerRepository, ICache cache)
            : base(innerRepository)
        {
            m_cache = cache;
            SearchResultLifetime = TimeSpan.FromSeconds(10);
            ItemLifetime = TimeSpan.FromMinutes(1);
        }

        public TimeSpan SearchResultLifetime { get; set; }

        public TimeSpan ItemLifetime { get; set; }

        public override IEnumerable<TicketType> ListTicketTypes(string languageCode)
        {
            return DefaultCache.Instance.Get(base.ListTicketTypes, languageCode);
        }

        public override IDictionary<int, TicketType> MapTicketType(string languageCode)
        {
            return DefaultCache.Instance.Get(base.MapTicketType, languageCode);
        }

        public override Ticket Retrieve(int identity)
        {
            var key = Keys.TicketKeyHelper.Retrieve(identity);
            return CacheHelper.Get(m_cache, key, () => base.Retrieve(identity), ItemLifetime);
        }

        public override void UpdateTicket(string username, Ticket ticket)
        {
            base.UpdateTicket(username, ticket);
            TicketDependencyHelper.Ticket(m_cache, ticket.TicketId);
        }

        public override RetrieveMultipleResponse<TicketSearchResult> RetrieveMultiple(RetrieveMultipleRequest<TicketSpecification> request)
        {
            var key = Keys.TicketKeyHelper.RetrieveMultiple(request);
            return CacheHelper.Get(m_cache, key, () => base.RetrieveMultiple(request), SearchResultLifetime, TicketDependency());
        }

        private LinkedCacheDependency TicketDependency()
        {
            return new LinkedCacheDependency(m_cache, 
                Keys.TicketKeyHelper.TicketDependency(), 
                DateTime.Now.Add(SearchResultLifetime));
        }
    }
}
