using System;
using System.Collections.Generic;

using ReusableLibrary.Abstractions.Caching;
using ReusableLibrary.Abstractions.Repository;
using ReusableLibrary.Supplemental.Caching;

using Tickets.Interface.Models;
using Tickets.Interface.Repository.Caching.Dependency;
using Tickets.Interface.Repository.Caching.Keys;

namespace Tickets.Interface.Repository.Caching
{
    public sealed class TicketRepository : Decorated.TicketRepository
    {
        private readonly ICache cache;

        public TicketRepository(ITicketRepository innerRepository, ICache cache)
            : base(innerRepository)
        {
            this.cache = cache;
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
            var key = TicketKeyHelper.Retrieve(identity);
            return CacheHelper.Get(cache, key, () => base.Retrieve(identity), ItemLifetime);
        }

        public override void UpdateTicket(string username, Ticket ticket)
        {
            base.UpdateTicket(username, ticket);
            TicketDependencyHelper.Ticket(cache, ticket.TicketId);
        }

        public override RetrieveMultipleResponse<TicketSearchResult> RetrieveMultiple(RetrieveMultipleRequest<TicketSpecification> request)
        {
            var key = TicketKeyHelper.RetrieveMultiple(request);
            return CacheHelper.Get(cache, key, () => base.RetrieveMultiple(request), SearchResultLifetime, TicketDependency());
        }

        private LinkedCacheDependency TicketDependency()
        {
            return new LinkedCacheDependency(cache, 
                TicketKeyHelper.TicketDependency(), 
                DateTime.Now.Add(SearchResultLifetime));
        }
    }
}