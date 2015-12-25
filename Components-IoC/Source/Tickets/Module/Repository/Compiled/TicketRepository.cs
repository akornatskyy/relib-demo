using System;
using System.Data.Linq;
using System.Linq;
using Tickets.Interface.Models;

namespace Tickets.Module.Repository
{
    public partial class TicketRepository
    {
        private static readonly Func<TicketsDataContext, string, IQueryable<TicketType>>
            FindAllTypesOrderedByNameQuery = CompiledQuery.Compile<TicketsDataContext, string, IQueryable<TicketType>>(
                (c, languageCode) => (from t in c.TicketTypes
                                      orderby t.Name
                                      select new TicketType(t.TicketTypeId, t.Name)));

        private static readonly Func<TicketsDataContext, int, Entities.Ticket>
            FindByIdQuery = CompiledQuery.Compile<TicketsDataContext, int, Entities.Ticket>(
                (c, identity) => (from t in c.Tickets
                                  where t.TicketId == identity
                                  select t).Single());
    }
}
