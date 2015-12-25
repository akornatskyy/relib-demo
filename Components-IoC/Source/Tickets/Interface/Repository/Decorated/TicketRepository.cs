using System.Collections.Generic;
using System.Diagnostics;
using ReusableLibrary.Abstractions.Repository;
using Tickets.Interface.Models;

namespace Tickets.Interface.Repository.Decorated
{
    public abstract class TicketRepository : ITicketRepository
    {
        private ITicketRepository m_innerRepository;

        protected TicketRepository(ITicketRepository innerRepository)
        {
            m_innerRepository = innerRepository;
        }

        #region ITicketRepository Members

        [DebuggerStepThrough]
        public virtual void UpdateTicket(string username, Ticket ticket)
        {
            m_innerRepository.UpdateTicket(username, ticket);
        }

        [DebuggerStepThrough]
        public virtual void AddHistory(string username, TicketHistory history)
        {
            m_innerRepository.AddHistory(username, history);
        }

        [DebuggerStepThrough]
        public virtual IEnumerable<TicketType> ListTicketTypes(string languageCode)
        {
            return m_innerRepository.ListTicketTypes(languageCode);
        }

        [DebuggerStepThrough]
        public virtual IDictionary<int, TicketType> MapTicketType(string languageCode)
        {
            return m_innerRepository.MapTicketType(languageCode);
        }

        #endregion

        #region IRetrieveRepository<int,Ticket> Members

        [DebuggerStepThrough]
        public virtual Ticket Retrieve(int identity)
        {
            return m_innerRepository.Retrieve(identity);
        }

        #endregion

        #region IRetrieveMultipleRepository<TicketSpecification,TicketSearchResult> Members

        [DebuggerStepThrough]
        public virtual RetrieveMultipleResponse<TicketSearchResult> RetrieveMultiple(RetrieveMultipleRequest<TicketSpecification> request)
        {
            return m_innerRepository.RetrieveMultiple(request);
        }

        #endregion
    }
}
