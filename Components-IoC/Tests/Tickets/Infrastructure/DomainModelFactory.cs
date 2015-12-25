using System;
using System.Collections.Generic;
using System.Linq;
using ReusableLibrary.Supplemental.System;
using Tickets.Interface.Models;

namespace Tickets.Tests.Infrastructure
{
    public static class DomainModelFactory
    {
        private static readonly Random g_random = new Random();
        private static readonly IEnumerable<string> g_users;

        static DomainModelFactory()
        {
            g_users = Properties.Resources.ListUsers
                .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList().AsReadOnly();
        }

        public static IEnumerable<string> RandomUsers()
        {
            return g_random.Shuffle(g_users);
        }

        public static IEnumerable<TicketSpecification> RandomTicketSpecifications()
        {
            var accounts = RandomUsers().ToArray();
            var lastDays = g_random.NextInt(7, 30);

            var start = DateTime.Today;
            var range = start.AddDays(lastDays).Subtract(start).Days;
            return g_random.NextSequence(500, 600, i =>
            {
                var to = g_random.NextDate(start, range).Date;
                return new TicketSpecification()
                {
                    IncludeClosed = g_random.NextBoolean(),
                    TicketTypeId = g_random.Next(1, 3, 5, 10),
                    Title = g_random.Next("computer", "mail", "install")
                };
            });
        }

        public static IEnumerable<string> RandomLanguageCodes()
        {
            return new[] { "en", "ru" };
        }
    }
}
