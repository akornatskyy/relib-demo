using System;
using System.Collections.Generic;
using System.Linq;

using ReusableLibrary.Abstractions.Repository;
using ReusableLibrary.Supplemental.Collections;
using ReusableLibrary.Supplemental.Repository;
using ReusableLibrary.Supplemental.System;

using Tickets.Interface.Models;
using Tickets.Interface.Repository;

namespace Tickets.Module.Repository.Mock
{
    public sealed class TicketRepository : ITicketRepository
    {
        private readonly Random random = new Random();

        #region ITicketRepository Members

        public void UpdateTicket(string username, Ticket ticket)
        {
        }

        public void AddHistory(string username, TicketHistory history)
        {
        }

        public IEnumerable<TicketType> ListTicketTypes(string languageCode)
        {
            var list = new List<TicketType>();
            if (languageCode == "ru")
            {
                list.AddRange(new[] 
                { 
                    new TicketType(0, "Любой"),
                    new TicketType(1, "Телекоммуникации"),
                    new TicketType(2, "Компьютер"),
                    new TicketType(3, "Принтер"),
                    new TicketType(4, "Сеть")
                });
            }
            else
            {
                list.AddRange(new[] 
                { 
                    new TicketType(0, "Any"),
                    new TicketType(1, "Telecommunications"),
                    new TicketType(2, "Computer"),
                    new TicketType(3, "Printer"),
                    new TicketType(4, "Network")
                });
            }

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
            return NextTicket();
        }

        #endregion

        #region IRetrieveMultipleRepository<TicketSpecification,TicketSearchResult> Members

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
            var tickets = new List<Ticket>();
            random.NextInt(50, 150).Times(() => tickets.Add(NextTicket()));
            return tickets.AsQueryable();
        }

        private Ticket NextTicket()
        {
            return new Ticket()
            {
                AssignedTo = "{0} {1}".FormatWith(random.NextWord().Capitalize(), random.NextWord().Capitalize()),
                ClosedOn = random.NextDate(-20),
                Description = random.NextSentences(3, 5, 5),
                IsOpen = random.NextBoolean(),
                ModifiedOn = random.NextDate(-40),
                OpenedOn = random.NextDate(-60),
                TicketId = random.NextInt(1000, 9999),
                TicketTypeId = random.NextInt(1, 4),
                Title = random.NextSentence(random.NextInt(4, 7))
            };
        }
    }
}