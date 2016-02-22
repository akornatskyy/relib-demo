using System.Collections.Generic;
using System.Diagnostics;

using ReusableLibrary.Abstractions.Repository;

using Tickets.Interface.Models;

namespace Tickets.Interface.Repository.Decorated
{
    public abstract class TicketRepository : ITicketRepository
    {
        private readonly ITicketRepository innerRepository;

        protected TicketRepository(ITicketRepository innerRepository)
        {
            this.innerRepository = innerRepository;
        }

        #region ITicketRepository Members

        [DebuggerStepThrough]
        public virtual void UpdateTicket(string username, Ticket ticket)
        {
            innerRepository.UpdateTicket(username, ticket);
        }

        [DebuggerStepThrough]
        public virtual void AddHistory(string username, TicketHistory history)
        {
            innerRepository.AddHistory(username, history);
        }

        [DebuggerStepThrough]
        public virtual IEnumerable<TicketType> ListTicketTypes(string languageCode)
        {
            return innerRepository.ListTicketTypes(languageCode);
        }

        [DebuggerStepThrough]
        public virtual IDictionary<int, TicketType> MapTicketType(string languageCode)
        {
            return innerRepository.MapTicketType(languageCode);
        }

        #endregion

        #region IRetrieveRepository<int,Ticket> Members

        [DebuggerStepThrough]
        public virtual Ticket Retrieve(int identity)
        {
            return innerRepository.Retrieve(identity);
        }

        #endregion

        #region IRetrieveMultipleRepository<TicketSpecification,TicketSearchResult> Members

        [DebuggerStepThrough]
        public virtual RetrieveMultipleResponse<TicketSearchResult> RetrieveMultiple(RetrieveMultipleRequest<TicketSpecification> request)
        {
            return innerRepository.RetrieveMultiple(request);
        }

        #endregion
    }
}