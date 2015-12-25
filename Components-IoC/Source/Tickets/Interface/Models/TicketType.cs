using System;
using ReusableLibrary.Abstractions.Models;

namespace Tickets.Interface.Models
{
    [Serializable]
    public sealed class TicketType : ValueObject<int>
    {
        public TicketType(int key, string displayName)
            : base(key, displayName)
        {
        }

        public static readonly TicketType None = new TicketType(-1, "None");

        public static readonly TicketType Any = new TicketType(0, "Any");
    }
}
