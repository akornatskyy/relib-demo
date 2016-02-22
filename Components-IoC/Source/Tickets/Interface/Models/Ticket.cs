using System;

namespace Tickets.Interface.Models
{
    [Serializable]
    public sealed class Ticket
    {
        public int TicketId { get; set; }

        public int TicketTypeId { get; set; }

        public TicketType TicketType { get; set; }

        public int AssignedToId { get; set; }

        public string AssignedTo { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsOpen { get; set; }

        public DateTime OpenedOn { get; set; }

        public DateTime? ClosedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}