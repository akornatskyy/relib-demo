using System;
using System.Collections.Generic;
using System.Web.Mvc;

using ReusableLibrary.Abstractions.Models;
using ReusableLibrary.Supplemental.Collections;
using ReusableLibrary.Supplemental.System;

using Tickets.Interface.Models;

namespace Public.WebMvcTests.Infrastructure
{
    public static class DomainModelFactory
    {
        private static readonly Random Rnd = new Random();

        public static IValueProvider ValueProvider(TicketSpecification spec)
        {
            return new SimpleValueProvider() 
            { 
                { "Title", spec.Title },
                { "TicketTypeId", spec.TicketTypeId },
                { "IncludeClosed", spec.IncludeClosed }
            };
        }

        public static TicketSpecification TicketSpecification()
        {
            return new TicketSpecification()
            {
                IncludeClosed = Rnd.NextBoolean(),
                TicketTypeId = Rnd.NextInt(0, 10),
                Title = Rnd.NextSentence(Rnd.NextInt(0, 5))
            };
        }

        public static IPagedList<TicketSearchResult> PagedList_TicketSearchResult(int count)
        {
            var items = new List<TicketSearchResult>();
            count.Times(() => items.Add(TicketSearchResult()));
            return new PagedList<TicketSearchResult>(items, 0, 10);
        }

        public static TicketSearchResult TicketSearchResult()
        {
            return new TicketSearchResult() 
            { 
                AssignedTo = Rnd.NextWord(),
                IsOpen = Rnd.NextBoolean(),
                TicketId = Rnd.NextInt(1, Int32.MaxValue - 1),
                TicketTypeId = Rnd.NextInt(0, 10),
                Title = Rnd.NextSentence(Rnd.NextInt(1, 5))
            };
        }
    }
}