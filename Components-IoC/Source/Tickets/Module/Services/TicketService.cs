using System.Collections.Generic;
using ReusableLibrary.Abstractions.Models;
using ReusableLibrary.Abstractions.Services;
using ReusableLibrary.Supplemental.Collections;
using ReusableLibrary.Supplemental.Repository;
using Tickets.Interface.Models;
using Tickets.Interface.Repository;
using Tickets.Interface.Services;

namespace Tickets.Module.Services
{
    public sealed class TicketService : AbstractService, ITicketService
    {
        private readonly IPagingSettings m_ticketPagingSettings;
        private readonly ITicketRepository m_ticketRepository;
        private readonly ILazy<IEnumerable<TicketType>> m_lazyTicketTypes;
        private readonly ILazy<IDictionary<int, TicketType>> m_lazyTicketTypeMap;

        public TicketService(IPagingSettings ticketPagingSettings, ITicketRepository ticketRepository)
        {
            m_ticketPagingSettings = ticketPagingSettings;
            m_ticketRepository = ticketRepository;
            m_lazyTicketTypes = new LazyObject<IEnumerable<TicketType>>(
                () => m_ticketRepository.ListTicketTypes(CurrentCulture.TwoLetterISOLanguageName));
            m_lazyTicketTypeMap = new LazyObject<IDictionary<int, TicketType>>(
                () => m_ticketRepository.MapTicketType(CurrentCulture.TwoLetterISOLanguageName));
        }

        #region ITicketService Members

        public TicketType DefaultTicketType
        {
            get { return TicketType.Any; }
        }

        public IEnumerable<TicketType> TicketTypes
        {
            get { return m_lazyTicketTypes.Object; }
        }

        public IDictionary<int, TicketType> TicketTypeMap
        {
            get { return m_lazyTicketTypeMap.Object; }
        }

        public IPagedList<TicketSearchResult> SearchTickets(TicketSpecification specification, int? pageIndex, int? pageSize)
        {
            return WithValid(specification, () =>
            {
                var result = m_ticketRepository.RetrieveMultiple(specification, pageIndex, pageSize, m_ticketPagingSettings);
                result.ForEach(t => t.TicketType = TicketTypeMap[t.TicketTypeId]);
                return result;
            });
        }

        public Ticket LoadTicket(int ticketId)
        {
            return WithValid(ValidateTicketIdentity(ticketId), () =>
                {
                    var result = m_ticketRepository.Retrieve(ticketId);
                    result.TicketType = TicketTypeMap[result.TicketTypeId];
                    return result;
                });
        }

        #endregion

        private static bool ValidateTicketIdentity(int identity)
        {
            return identity > 0;
        }
    }
}
