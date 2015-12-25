using System.Collections.Generic;
using ReusableLibrary.Abstractions.Repository;
using Tickets.Interface.Models;

namespace Tickets.Interface.Repository
{
    public interface ITicketRepository : IRetrieveRepository<int, Ticket>, 
        IRetrieveMultipleRepository<TicketSpecification, TicketSearchResult>
    {
        void UpdateTicket(string username, Ticket ticket);

        void AddHistory(string username, TicketHistory history);

        IEnumerable<TicketType> ListTicketTypes(string languageCode);

        IDictionary<int, TicketType> MapTicketType(string languageCode);
    }
}
