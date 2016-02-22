using System;

namespace Tickets.Interface.Models
{
    [Serializable]
    public sealed class TicketHistory
    {
        public int TicketHistoryId { get; set; }
        
        public int StaffId { get; set; }
        
        public int TicketId { get; set; }
        
        public string Comment { get; set; }
        
        public DateTime CreatedOn { get; set; }
    }
}