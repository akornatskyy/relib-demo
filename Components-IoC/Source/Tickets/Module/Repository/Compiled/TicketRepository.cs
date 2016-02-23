using System;
using System.Data.Linq;
using System.Linq;

using Tickets.Module.Repository.Entities;

namespace Tickets.Module.Repository
{
    public partial class TicketRepository
    {
        private static readonly Func<TicketsDataContext, string, IQueryable<Interface.Models.TicketType>>
            FindAllTypesOrderedByNameQuery = CompiledQuery.Compile<TicketsDataContext, string, IQueryable<Interface.Models.TicketType>>(
                (c, languageCode) => (from t in c.TicketTypes
                                      orderby t.Name
                                      select new Interface.Models.TicketType(t.TicketTypeId, t.Name)));

        private static readonly Func<TicketsDataContext, int, Ticket>
            FindByIdQuery = CompiledQuery.Compile<TicketsDataContext, int, Ticket>(
                (c, identity) => (from t in c.Tickets
                                  where t.TicketId == identity
                                  select t).Single());
    }
}