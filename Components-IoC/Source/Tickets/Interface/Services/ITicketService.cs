using System.Collections.Generic;

using ReusableLibrary.Abstractions.Models;

using Tickets.Interface.Models;

namespace Tickets.Interface.Services
{
    public interface ITicketService
    {
        TicketType DefaultTicketType { get; }

        IEnumerable<TicketType> TicketTypes { get; }

        IDictionary<int, TicketType> TicketTypeMap { get; }

        IPagedList<TicketSearchResult> SearchTickets(TicketSpecification specification, int? pageIndex, int? pageSize);

        Ticket LoadTicket(int ticketId);
    }
}