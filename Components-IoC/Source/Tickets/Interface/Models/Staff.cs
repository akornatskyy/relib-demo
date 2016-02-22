using System;

namespace Tickets.Interface.Models
{
    [Serializable]
    public sealed class Staff
    {
        public int StaffId { get; set; }

        public string DisplayName { get; set; }

        public int ReportsTo { get; set; }
    }
}