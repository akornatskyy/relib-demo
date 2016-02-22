using System;
using System.Linq.Expressions;

using ReusableLibrary.Abstractions.Models;
using ReusableLibrary.Supplemental.Models;

namespace Tickets.Interface.Models
{
    [Serializable]
    public sealed class TicketSpecification : ISpecification<Ticket>, IKeyProvider
    {
        public TicketSpecification()
        {
            Title = string.Empty;
            TicketTypeId = TicketType.Any.Key;
        }

        public string Title { get; set; }

        public int TicketTypeId { get; set; }

        public bool IncludeClosed { get; set; }

        #region ISpecification<Ticket> Members

        public Expression<Func<Ticket, bool>> IsSatisfied()
        {
            return t => t.Title.Contains(Title)
                && (TicketTypeId == TicketType.Any.Key || t.TicketTypeId == TicketTypeId)
                && (IncludeClosed || t.IsOpen);
        }

        #endregion

        #region IKeyProvider Members

        public string ToKeyString()
        {
            return string.Join(":", "TS", Title, TicketTypeId, IncludeClosed);
        }

        #endregion
    }
}