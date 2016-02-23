using System.Collections.Generic;
using System.Linq;

using ReusableLibrary.Abstractions.Repository;
using ReusableLibrary.Supplemental.Collections;
using ReusableLibrary.Supplemental.Repository;

using Tickets.Interface.Models;
using Tickets.Interface.Repository;

namespace Tickets.Module.Repository
{
    public sealed partial class TicketRepository : ITicketRepository
    {
        private readonly TicketsDataContext context;

        public TicketRepository(TicketsDataContext context)
        {
            this.context = context;
        }

        #region ITicketRepository Members

        public void UpdateTicket(string username, Ticket ticket)
        {
        }

        public void AddHistory(string username, TicketHistory history)
        {
        }

        public IEnumerable<TicketType> ListTicketTypes(string languageCode)
        {
            var list = FindAllTypesOrderedByNameQuery.Invoke(context, languageCode).ToList();
            list.Insert(0, TicketType.Any);
            return list.AsReadOnly();
        }

        public IDictionary<int, TicketType> MapTicketType(string languageCode)
        {
            return ListTicketTypes(languageCode).ToDictionary<int, TicketType>();
        }

        #endregion

        #region IRetrieveRepository<int,Ticket> Members

        public Ticket Retrieve(int identity)
        {
            var t = FindByIdQuery.Invoke(context, identity);
            return new Ticket()
            {
                AssignedTo = t.AssignedTo.DisplayName,
                AssignedToId = t.AssignedToId,
                ClosedOn = t.ClosedOn,
                Description = t.Description,
                IsOpen = t.IsOpen,
                ModifiedOn = t.ModifiedOn,
                OpenedOn = t.OpenedOn,
                TicketId = t.TicketId,
                TicketTypeId = t.TicketTypeId,
                Title = t.Title
            };
        }

        #endregion

        #region IRetrieveMultipleRepository<TicketSearchSpecification,TicketSearchResult> Members

        public RetrieveMultipleResponse<TicketSearchResult> RetrieveMultiple(RetrieveMultipleRequest<TicketSpecification> request)
        {
            return AllTickets()
                .Satisfy(request.Specification)
                .Select(t => new TicketSearchResult()
                {
                    AssignedTo = t.AssignedTo,
                    IsOpen = t.IsOpen,
                    TicketId = t.TicketId,
                    TicketTypeId = t.TicketTypeId,
                    Title = t.Title
                })
                .SatisfyPaging(request.PageIndex, request.PageSize)
                .ToResponse(request.PageSize);
        }

        #endregion

        private IQueryable<Ticket> AllTickets()
        {
            return from t in context.Tickets
                   orderby t.TicketTypeId, t.OpenedOn descending
                   select new Ticket()
                   {
                       AssignedTo = t.AssignedTo.DisplayName,
                       AssignedToId = t.AssignedToId,
                       ClosedOn = t.ClosedOn,
                       Description = t.Description,
                       IsOpen = t.IsOpen,
                       ModifiedOn = t.ModifiedOn,
                       OpenedOn = t.OpenedOn,
                       TicketId = t.TicketId,
                       TicketTypeId = t.TicketTypeId,
                       Title = t.Title
                   };
        }
    }
}