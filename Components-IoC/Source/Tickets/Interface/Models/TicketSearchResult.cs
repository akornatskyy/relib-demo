using System;

namespace Tickets.Interface.Models
{
    [Serializable]
    public sealed class TicketSearchResult
    {
        public int TicketId { get; set; }

        public int TicketTypeId { get; set; }

        public TicketType TicketType { get; set; }

        public string AssignedTo { get; set; }

        public string Title { get; set; }

        public bool IsOpen { get; set; }
    }
}