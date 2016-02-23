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
        private readonly IPagingSettings ticketPagingSettings;
        private readonly ITicketRepository ticketRepository;
        private readonly ILazy<IEnumerable<TicketType>> lazyTicketTypes;
        private readonly ILazy<IDictionary<int, TicketType>> lazyTicketTypeMap;

        public TicketService(IPagingSettings ticketPagingSettings, ITicketRepository ticketRepository)
        {
            this.ticketPagingSettings = ticketPagingSettings;
            this.ticketRepository = ticketRepository;
            lazyTicketTypes = new LazyObject<IEnumerable<TicketType>>(
                () => this.ticketRepository.ListTicketTypes(CurrentCulture.TwoLetterISOLanguageName));
            lazyTicketTypeMap = new LazyObject<IDictionary<int, TicketType>>(
                () => this.ticketRepository.MapTicketType(CurrentCulture.TwoLetterISOLanguageName));
        }

        #region ITicketService Members

        public TicketType DefaultTicketType
        {
            get { return TicketType.Any; }
        }

        public IEnumerable<TicketType> TicketTypes
        {
            get { return lazyTicketTypes.Object; }
        }

        public IDictionary<int, TicketType> TicketTypeMap
        {
            get { return lazyTicketTypeMap.Object; }
        }

        public IPagedList<TicketSearchResult> SearchTickets(TicketSpecification specification, int? pageIndex, int? pageSize)
        {
            return WithValid(specification, () =>
            {
                var result = ticketRepository.RetrieveMultiple(specification, pageIndex, pageSize, ticketPagingSettings);
                result.ForEach(t => t.TicketType = TicketTypeMap[t.TicketTypeId]);
                return result;
            });
        }

        public Ticket LoadTicket(int ticketId)
        {
            return WithValid(ValidateTicketIdentity(ticketId), () =>
                {
                    var result = ticketRepository.Retrieve(ticketId);
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