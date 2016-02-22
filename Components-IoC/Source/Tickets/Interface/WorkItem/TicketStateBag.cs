using Tickets.Interface.Models;

namespace Tickets.Interface.WorkItem
{
    public sealed class TicketStateBag
    {
        public int Number { get; set; }

        public Ticket Item { get; set; }
    }
}